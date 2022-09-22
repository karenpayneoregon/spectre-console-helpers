using Microsoft.EntityFrameworkCore;
using SelectiveUpdatesApp.Data;

namespace SelectiveUpdatesApp
{
    internal partial class Program
    {
        static void Main(string[] args)
        {
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
            
            AnsiConsole.MarkupLine("[yellow]Done[/]");
            Console.ReadLine();
        }
    }
}