# About

Simple example that reads a record, makes changes which since change tracker is tracking the entity on save all changed properties are sent to the database but in this example we mark `context.Entry(person).Property(p => p.LastName).IsModified = false;` to not send changes for LastName to the database, only FirstName.


```csharp
using var context = new Context();
var person = context.Person.FirstOrDefault();

if (person is not null)
{
    person.FirstName = "James";
    person.LastName = "Adams";
    context.Entry(person).State = EntityState.Modified;
    context.Entry(person).Property(p => p.LastName).IsModified = false;
    context.SaveChanges();
}
```

> **Note**
> Run executing the code, review the logging in Visual Studio Output window.