using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1.Models;

namespace ConsoleApp1.Classes
{
    internal class DataOperations
    {
        public static void ReadTaxpayersUnsorted()
        {
            AnsiConsole.MarkupLine($"[b]Running[/] [cyan]{nameof(ReadTaxpayersUnsorted)}[/]");

            var taxpayers = FileOperations.ReadTaxpayers();

            foreach (var taxpayer in taxpayers)
            {
                Console.WriteLine(taxpayer.LastName);
            }
        }
        public static void ReadTaxpayersSorted()
        {
            AnsiConsole.MarkupLine($"[b]Running[/] [cyan]{nameof(ReadTaxpayersSorted)}[/]");

            var taxpayers = FileOperations
                    .ReadTaxpayers()
                    .OrderBy(x => x.LastName);

            foreach (var taxpayer in taxpayers)
            {
                Console.WriteLine(taxpayer.LastName);
            }
        }

        public static void ReadTaxpayersLastNameStartsWithWrong()
        {
            AnsiConsole.MarkupLine($"[b]Running[/] [cyan]{nameof(ReadTaxpayersLastNameStartsWithWrong)}[/]");

            var startsWith = "h";

            var taxpayers = FileOperations
                    .ReadTaxpayers()
                    .Where(x => x.LastName.StartsWith(startsWith));


            // ReSharper disable once PossibleMultipleEnumeration
            if (taxpayers.Any())
            {
                foreach (var taxpayer in taxpayers)
                {
                    Console.WriteLine(taxpayer.LastName);
                }
            }
            else
            {
                Console.WriteLine($"No last names that start with {startsWith}");
            }
        }

        public static void ReadTaxpayersLastNameStartsWithRight()
        {
            AnsiConsole.MarkupLine($"[b]Running[/] [cyan]{nameof(ReadTaxpayersLastNameStartsWithRight)}[/]");

            var startsWith = "h";

            var taxpayers = FileOperations
                    .ReadTaxpayers()
                    .Where(x => x.LastName.StartsWith(startsWith,
                        StringComparison.OrdinalIgnoreCase));


            // ReSharper disable once PossibleMultipleEnumeration
            if (taxpayers.Any())
            {

                foreach (var taxpayer in taxpayers)
                {
                    Console.WriteLine(taxpayer.LastName);
                }
            }
            else
            {
                Console.WriteLine($"No last names that start with {startsWith}");
            }
        }

        /// <summary>
        /// Simple group-by example
        /// * read all taxpayers, order by last name
        /// * group by category id
        /// </summary>
        public static void GroupByExample()
        {
            AnsiConsole.MarkupLine($"[b]Running[/] [cyan]{nameof(GroupByExample)}[/]");
            Console.WriteLine();

            var categories = FileOperations.ReadCategories();

            IOrderedEnumerable<IGrouping<int, Taxpayer>> taxpayers = FileOperations
                    .ReadTaxpayers()
                    .OrderBy(tp => tp.LastName)
                    .GroupBy(tp => tp.CategoryId)
                    .OrderBy(iGroup => iGroup.FirstOrDefault()?.Category.Description)
                    .ThenBy(iGroup => iGroup.FirstOrDefault()?.LastName);

            /*
             * In general, var would be best rather than IGrouping<int, Taxpayer> but
             * for learning purposes let's show the type.
             *
             */
            foreach (IGrouping<int, Taxpayer> grouped in taxpayers)
            {

                AnsiConsole.MarkupLine(
                    $"[yellow]{categories.FirstOrDefault(x => x.CategoryId == grouped.Key)!.Description}[/]");

                foreach (Taxpayer taxpayer in grouped)
                {

                    if (taxpayer.StartDate.HasValue)
                    {
                        Console.WriteLine($"\t{taxpayer.FullName,-20}{taxpayer.StartDate.Value,-12:MM/dd/yyyy}" +
                                          $"{taxpayer.SocialSecurityNumber}");
                    }
                }
            }
        }
    }
}
