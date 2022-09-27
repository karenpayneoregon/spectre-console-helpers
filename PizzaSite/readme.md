# About

This is a basic ASP.NET Core [^1] project using Razor Pages [^2] using Entity Framework Core 6 [^3] using Microsoft SQL-Server database.

# Project structure

Functionality is broken up as follows

## wwwroot

CSS, JavaScript libraries

## Pages/Shared

Site wide configuration

## Pages/Views

The `Pages` folder contains pages for each model [^4]. Under each folder e.g. the `Customers` folder are several pages, for

- Viewing data (list all records)
- Details view of data (one record)
- Editing data
- Removal of data

For this project I left some pages disconected or left out to show (if time permits) how to create a new page.

# Libraries

Visual Studio has a component, `libman` which allows adding external libraries such as OED libraries and/or libraries such as BootStrap (ASP.NET Core comes with the current release of BootStrap).

**libman**

![Libman](assets/libman.png)

# Entity Framework Core

- DbContext (under the `Data` folder)
    - Manage database connection
    - Configure model & relationship
    - Querying database
    - Saving data to the database
    - Configure change tracking
    - Caching
    - Transaction management

Samples were `context` is a `DbContext`

Get a `Customer` and their orders

```csharp
public static async Task<Customer> GetCustomer(int id)
{
    await using var context = new PizzaContext();
    return await context
        .Customers
        .Include(c => c.Orders)
        .FirstOrDefaultAsync(c => c.Id == id);
}
```

Update a `Customer` where the code is designed to update only first and last name. Caveat, the line with `IsModified = false` is ceremonial in that Entity Framework Core is smart enough on it's own to know what to update but in earlier versions we needed `IsModified = false`.

```csharp
public static async Task UpdateName(Customer currentCustomer)
{
    await using var context = new PizzaContext();
    var customer = context.Customers.FirstOrDefault(c => c.Id == currentCustomer.Id);

    if (customer is not null)
    {
        customer.FirstName = currentCustomer.FirstName;
        customer.LastName = currentCustomer.LastName;
        context.Entry(customer).State = EntityState.Modified;
        context.Entry(customer).Property(p => p.Email).IsModified = false;
        await context.SaveChangesAsync();
    }
}
```




- Models (under the `Models` folder)




# Database schema

![Database Model](assets/DatabaseModel.png)






[^1]: Overview to [ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/introduction-to-aspnet-core?view=aspnetcore-6.0)
[^2]:Introduction to [Razor Pages](https://learn.microsoft.com/en-us/aspnet/core/razor-pages/?view=aspnetcore-6.0&tabs=visual-studio) in ASP.NET Core
[^3]: Entity Framework Core 6
[^4]: A [model](https://learn.microsoft.com/en-us/ef/core/#the-model) is made up of entity classes and a context object that represents a session with the database. 