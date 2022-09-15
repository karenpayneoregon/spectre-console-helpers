# About

![Screen Shot](assets/screenShot.png)

This project demonstrates dynamically creating a SQL-Server LocalDb database using EF Core and Bogus.

Not something an enterprise developer would do, instead the better method is to create a database in SSMS than reverse engineer the database using a Visual Studio extension like EF Power Tools.

> **Note**
> There are no free extensions or tools to reverse engineer Oracle. A developer needs to use the dotnet CLI tools to scaffold an Oracle database or create your own tool such as one Karen created shown below.

![Oracle](assets/oracle.png)

# Generating database on the fly

This is done by EF Core looking to the DbContext, get the DbSet objects, looks at the models for the DbSet objects then if a model has an int property named Id or ends in Id creates primary and foreign keys which is also dependent on properties that are from another model which can be a single instance or a list.

Given

```csharp
public partial class Taxpayer
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string FullName => $"{FirstName} {LastName}";
    public string SSN { get; set; }
    public string SocialSecurityNumber => SSN.Insert(5, "-").Insert(3, "-");
    public string Pin { get; set; }
    public DateTime? StartDate { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; }
    public override string ToString() => $"{FirstName} {LastName}";
}

public class Category
{
    [Key]
    public int CategoryId { get; set; }
    public string Description { get; set; }
    public List<Taxpayer> Taxpayers { get; set; }

    public Category()
    {
        Taxpayers = new List<Taxpayer>();
    }
}
```

EF Core generates

![Diagram](assets/diagram.png)

