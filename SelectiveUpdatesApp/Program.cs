using Microsoft.EntityFrameworkCore;
using SelectiveUpdatesApp.Data;
using SelectiveUpdatesApp.Models;

namespace SelectiveUpdatesApp
{
    internal partial class Program
    {
        static void Main(string[] args)
        {
            FirstExample();
            SecondExample();
            AnsiConsole.MarkupLine("[white on blue]Time to exit[/]");
            Console.ReadLine();
        }

        private static void FirstExample()
        {
            AnsiConsole.MarkupLine($"[cyan]Running[/] [yellow]{nameof(FirstExample)}[/]");

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

            AnsiConsole.MarkupLine($"[cyan]Done[/] [yellow]{nameof(FirstExample)}[/]");

        }

        private static void SecondExample()
        {
            AnsiConsole.MarkupLine($"[cyan]Running[/] [yellow]{nameof(SecondExample)}[/]");

            using var context = new Context();
            int identifier = 2;
            var person = new Person() { Id = identifier };
            PersonModel model = new PersonModel() { Id = identifier, FirstName = "Kate" };

            context.Attach(person);
            context.Entry(person).CurrentValues.SetValues(model); /// reflection
            context.SaveChanges();

            AnsiConsole.MarkupLine($"[cyan]Done[/] [yellow]{nameof(SecondExample)}[/]");

        }
    }
}