using Microsoft.EntityFrameworkCore;
using SelectiveUpdatesApp.Data;
using SelectiveUpdatesApp.Models;

namespace SelectiveUpdatesApp
{
    internal partial class Program
    {
        static void Main(string[] args)
        {

            using Context context = new();
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            FirstExample();
            SecondExample();
            ThirdExample(5, new PersonDto { Title = "Miss", FirstName = "Karen", LastName = "Payne", BirthDate = new DateTime(1956,9,24)});
            AnsiConsole.MarkupLine("[white on blue]Time to exit[/]");
            Console.ReadLine();
        }

        private static void FirstExample()
        {
            AnsiConsole.MarkupLine($"[cyan]Running[/] [yellow]{nameof(FirstExample)}[/]");

            using Context context = new ();
            Person person = context.Person.FirstOrDefault();

            bool modifyLastName = person.LastName != "Gallagher";

            if (person is not null)
            {
                person.FirstName = "James";
                person.LastName = "Adams";
                context.Entry(person).State = EntityState.Modified;
                context.Entry(person).Property(p => p.LastName).IsModified = modifyLastName;
                context.SaveChanges();
            }

            AnsiConsole.MarkupLine($"[cyan]Done[/] [yellow]{nameof(FirstExample)}[/]");

        }

        private static void SecondExample()
        {
            AnsiConsole.MarkupLine($"[cyan]Running[/] [yellow]{nameof(SecondExample)}[/]");

            using Context context = new ();
            int identifier = 2;
            Person person = new () { Id = identifier };
            PersonModel model = new() { Id = identifier, FirstName = "Karen" };

            context.Attach(person);
            context.Entry(person).CurrentValues.SetValues(model);
            context.SaveChanges();

            AnsiConsole.MarkupLine($"[cyan]Done[/] [yellow]{nameof(SecondExample)}[/]");

        }


        private static void ThirdExample(int id, PersonDto sender)
        {
            AnsiConsole.MarkupLine($"[cyan]Running[/] [yellow]{nameof(ThirdExample)}[/]");

            using Context context = new();
            int identifier = id;
            Person person = new() { Id = identifier };

            context.Attach(person);
            context.Entry(person).CurrentValues.SetValues(sender);
            context.SaveChanges();

            AnsiConsole.MarkupLine($"[cyan]Done[/] [yellow]{nameof(ThirdExample)}[/]");

        }
    }
}