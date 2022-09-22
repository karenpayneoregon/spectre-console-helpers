using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using SelectiveUpdatesApp.Data;
using SelectiveUpdatesApp.Models;

// ReSharper disable once CheckNamespace
namespace SelectiveUpdatesApp
{
    internal partial class Program
    {
        [ModuleInitializer]
        public static void Init()
        {
            AnsiConsole.MarkupLine("");
            Console.Title = "Code sample";
            WindowUtility.SetConsoleWindowPosition(WindowUtility.AnchorWindow.Center);
            CleanDatabase();
            Populate();
            AnsiConsole.MarkupLine("[yellow]Populated[/]");
        }
        public static void CleanDatabase()
        {
            using var context = new Context();
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }

        private static void Populate()
        {
            using var context = new Context();
            context.Add(new Person() {FirstName = "Jim", LastName = "Gallagher", Title = "Mr", BirthDate = new DateTime(1957,8,7)});
            context.Add(new Person() {FirstName = "Maggie", LastName = "Gallagher", Title = "Mrs", BirthDate = new DateTime(1960,5,11)});
            context.Add(new Person() {FirstName = "Billy", LastName = "Smith", Title = "Mr", BirthDate = new DateTime(1989,9,23)});
            context.SaveChanges();
        }
    }
}
