
# Connect to a SQL-Server database using a managed data provider

The basics for connecting to a database requires a connection string which is used in a connection object, [SqlConnection](https://docs.microsoft.com/en-us/dotnet/api/system.data.sqlclient.sqlconnection?view=dotnet-plat-ext-6.0) (sqlclient is the data provider) in this case for SQL-Server, localDb[^1].

```
Server=(localdb)\\MSSQLLocalDB;Database=OED;Trusted_Connection=True
```

The connection string is passed to the constructor for a `SqlConnection` object as follows.

```csharp
public static async Task ConnectToDatabase()
{
    string connectionString = 
        "Server=(localdb)\\MSSQLLocalDB;Database=OED;Trusted_Connection=True";

    await using var cn = new SqlConnection(ConfigurationHelper.ConnectionString());
    await cn.OpenAsync();
}
```

In enterprise applications the connection is not stored as shown above, instead stored in a central location which would look like.

```csharp
public static async Task ConnectToDatabase()
{
    await using var cn = new SqlConnection(ConfigurationHelper.ConnectionString());
    await cn.OpenAsync();
}
```

Keeping things simple, in the above `ConfigurationHelper.ConnectionString()` is an Nuget package which reads a settings file.

```json
{
  "ConnectionsConfiguration": {
    "ActiveEnvironment": "Development",
    "Development": "Server=(localdb)\\MSSQLLocalDB;Database=OED;Trusted_Connection=True",
    "Stage": "Stage connection string goes here",
    "Production": "Prod connection string goes here"
  }
}
```

Currently the connection is for `Development` as per `ActiveEnvironment`. Of course connection strings can be stored in other containers and still accessed using a the same method shown above configured for where the connection strings are stroed.

**How does FAST do it?** Not with hard coded connection strings.

We can also test if a connection works using code similar to the following where the [CancellationToken](https://docs.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken?view=net-6.0) has a timeout set by the caller `new CancellationTokenSource(TimeSpan.FromSeconds(4))` which means after four seconds thow a [TaskCanceledException](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.taskcanceledexception?view=net-6.0) or for developer screw-up an [Exception](https://docs.microsoft.com/en-us/dotnet/api/system.exception?view=net-6.0).

```csharp
public static async Task TestConnection(CancellationToken ct)
{
    try
    {
        await using var cn = new SqlConnection(ConfigurationHelper.ConnectionString());
        await cn.OpenAsync(ct);
    }
    catch (TaskCanceledException tce)
    {

        // handle timeout
    }
    catch (Exception localException)
    {
        // handle general exceptions
    }
}
```

# Read data from a SQL-Sever database table(s)

To read data from a table we need a [SqlConnection](https://docs.microsoft.com/en-us/dotnet/api/system.data.sqlclient.sqlconnection?view=dotnet-plat-ext-6.0) and a [SqlCommand](https://docs.microsoft.com/en-us/dotnet/api/system.data.sqlclient.sqlcommand?view=dotnet-plat-ext-6.0) object.

## Model

Also need a model/class, in this case a Taxpayer model.

```csharp
public class Taxpayer
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string SSN { get; set; }
    public string Pin { get; set; }
    public DateOnly? StartDate { get; set; }
}
```

## Code to read data


```csharp
public static async Task<(Taxpayer taxpayer, bool found)> GetTaxpayer(int id)
{
    var statement = 
        "SELECT FirstName,LastName,SSN,Pin,StartDate " +
        "FROM dbo.Taxpayer WHERE Id = @Id";

    await using var cn = new SqlConnection(ConfigurationHelper.ConnectionString());
    await using var cmd = new SqlCommand { Connection = cn, CommandText = statement };
    cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;

    await cn.OpenAsync();
    await using var reader = await cmd.ExecuteReaderAsync();

    if (reader.HasRows)
    {
        reader.Read();
        return (new Taxpayer()
        {
            Id = id, 
            FirstName = reader.GetString(0),
            LastName = reader.GetString(1),
            SSN = reader.GetString(2),
            Pin = reader.GetString(3),
            StartDate = DateOnly.FromDateTime(reader.GetDateTime(4))
        },true);
    }
    else
    {
        return (null, false);
    }
            
}
```

1. `statement` is the SQL SELECT to get a `Taxpayer` by primary key
1. `cn` is responsible for connecting to the database
1. `cmd` is the object use to make calls to the database
1. ` cmd.Parameters.Add` is needed for each parameter in the SQL
1. `reader` ask for data
1. `reader.HasRows` ask, was there a record with the primary key we are looking for
1. `reader.Read()` reads one record. If we had no records and bypassed `reader.HasRows` a runtime exception is thrown.
1. The `return` statement builds the `Taxpayer` from data read from the `reader`.


## Code to insert data

The same pattern as above is used here accept the query statement is different and there is a secondary query. The first query inserts data while the second returns the new primary key.

```csharp
public static async Task<(bool success, Exception exception)> AddNewTaxpayer(Taxpayer taxpayer)
{

    var statement = @"
INSERT INTO [dbo].[Taxpayer]  ([FirstName],[LastName],[SSN],[Pin],[StartDate]) 
VALUES (@FirstName,@LastName,@SSN,@Pin,@StartDate);
SELECT CAST(scope_identity() AS int);
";

    await using var cn = new SqlConnection(ConfigurationHelper.ConnectionString());
    await using var cmd = new SqlCommand { Connection = cn, CommandText = statement };

    cmd.Parameters.Add("@FirstName", SqlDbType.NVarChar).Value = taxpayer.FirstName;
    cmd.Parameters.Add("@LastName", SqlDbType.NVarChar).Value = taxpayer.LastName;
    cmd.Parameters.Add("@SSN", SqlDbType.NVarChar).Value = taxpayer.SSN;
    cmd.Parameters.Add("@Pin", SqlDbType.NVarChar).Value = taxpayer.Pin;
    cmd.Parameters.Add("@StartDate", SqlDbType.Date).Value =  
        taxpayer.StartDate!.Value.ToDateTime(new TimeOnly(0,0,0));

    try
    {
        await cn.OpenAsync();
        taxpayer.Id = Convert.ToInt32(cmd.ExecuteScalar());
        return (true, null);

    }
    catch (Exception localException)
    {
        return (false, localException);
    }
}
```

## Edit an existing record

Do you see a pattern forming in regards to what it takes to perform, in this case updating an existing record?

Note, to keep things simple only one property is changed using a random date/time which is converted to a DateOnly object.

```csharp
public static async Task<(Taxpayer taxpayer, bool)> EditTaxpayer()
{
    int id = 1;
    var newStartDate = "2022-09-14";
    RandomDateTime date = new RandomDateTime();
    var value = date.DateValue(date.Next());
            
    var (taxpayer, found) = await GetTaxpayer(id);
    if (found)
    {
        taxpayer.StartDate = DateOnly.Parse(value);
        await using var cn = new SqlConnection(ConfigurationHelper.ConnectionString());
        string statement = "UPDATE [dbo].[Taxpayer] SET StartDate = @StartDate WHERE Id = @Id";
        await using var cmd = new SqlCommand { Connection = cn, CommandText = statement };
        cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;
        cmd.Parameters.Add("@StartDate", SqlDbType.Date).Value = newStartDate;

        await cn.OpenAsync();
        await cmd.ExecuteNonQueryAsync();

        return (taxpayer, true);

    }
    else
    {
        return (null, false);
    }
}
```

# New school Entity Framework Core

All of the code which follows matches above data provider samples which has many benefits and is Microsoft's flagship methods to work with data.

## Get one Taxpayer

```csharp
public static async Task<Taxpayer> GetTaxpayerByIdentity(int id)
{
    await using var context = new OedContext();
    return (await context.Taxpayer.FirstOrDefaultAsync(payer => payer.Id == id))!;
}
```

## Add new Taxpayer

```csharp
public static async Task AddNewTaxpayer(Taxpayer taxpayer)
{
    await using var context = new OedContext();
    context.Add(taxpayer);
    await context.SaveChangesAsync();
}
```

## Remove a Taxpayer

Looking at add, can you guess how to remove a record?

## Edit a Taxpayer

```csharp
public static async Task<(Taxpayer taxpayer, bool)> EditTaxpayer()
{
    await using var context = new OedContext();
    int id = 3;
    Taxpayer taxpayer = context.Taxpayer.FirstOrDefault(x => x.Id == id)!;
    if (taxpayer is not null)
    {
        RandomDateTime date = new RandomDateTime();
        taxpayer.StartDate = date.Next();
        await context.SaveChangesAsync();
        return (taxpayer, true);
    }
    else
    {
        return (null, false)!;
    }
}
```


### Special note

In the database table for Taxpayer, there is a `date` column which in the .NET side use to map to a [DateTime](https://docs.microsoft.com/en-us/dotnet/api/system.datetime?view=net-6.0) struct which means the time is included which will alway be midnight. We don't need the time so instead a new type is used, [DateOnly](https://docs.microsoft.com/en-us/dotnet/api/system.dateonly?view=net-6.0) but the data provider knows nothing about DateOnly so the following transforms the DateTime to a DateOnly `DateOnly.FromDateTime(reader.GetDateTime(4))`





[^1]: What is LocalDB? It is a new version of SQL Server Express dedicated to developers to help them avoid a full installation of other editions of SQL Server. LocalDb should never be used for enterprise applications. It is easy to port from LocalDb to a enterprise database.

