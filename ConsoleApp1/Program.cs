using System.Security.Cryptography.X509Certificates;
using ConsoleApp1.Classes;
using ConsoleApp1.Models;
using Spectre.Console;

namespace ConsoleApp1
{
    internal partial class Program
    {
        static void Main()
        {
            //ReadTaxpayersLastNameStartsWithRight();
            GroupByExample();

            AnsiConsole.MarkupLine("[white on blue]Press a key to exit[/]");
            Console.WriteLine();
            Console.ReadLine();
        }

        private static void ReadTaxpayersUnsorted()
        {
            AnsiConsole.MarkupLine($"[b]Running[/] [cyan]{nameof(ReadTaxpayersUnsorted)}[/]");

            var taxpayers = Operations.ReadTaxpayers();

            foreach (var taxpayer in taxpayers)
            {
                Console.WriteLine(taxpayer.LastName);
            }
        }
        private static void ReadTaxpayersSorted()
        {
            AnsiConsole.MarkupLine($"[b]Running[/] [cyan]{nameof(ReadTaxpayersSorted)}[/]");

            var taxpayers = Operations
                    .ReadTaxpayers()
                    .OrderBy(x => x.LastName);

            foreach (var taxpayer in taxpayers)
            {
                Console.WriteLine(taxpayer.LastName);
            }
        }

        private static void ReadTaxpayersLastNameStartsWithWrong()
        {
            AnsiConsole.MarkupLine($"[b]Running[/] [cyan]{nameof(ReadTaxpayersLastNameStartsWithWrong)}[/]");

            var startsWith = "h";

            var taxpayers = Operations
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
        private static void ReadTaxpayersLastNameStartsWithRight()
        {
            AnsiConsole.MarkupLine($"[b]Running[/] [cyan]{nameof(ReadTaxpayersLastNameStartsWithRight)}[/]");

            var startsWith = "h";

            var taxpayers = Operations
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
        private static void GroupByExample()
        {
            AnsiConsole.MarkupLine($"[b]Running[/] [cyan]{nameof(GroupByExample)}[/]");
            Console.WriteLine();

            var categories = Operations.ReadCategories();

            IOrderedEnumerable<IGrouping<int, Taxpayer>> taxpayers = Operations
                    .ReadTaxpayers()
                    .OrderBy(tp => tp.LastName)
                    .GroupBy(tp => tp.CategoryId)
                    .OrderBy(iGroup => iGroup.FirstOrDefault()?.Category.Description)
                    .ThenBy(iGroup => iGroup.FirstOrDefault()?.LastName);

            
            /*
             * In general, var would be best rather than IGrouping<int, Taxpayer> but
             * for learning purposes let's show the type.
             */
            foreach (IGrouping<int, Taxpayer> grouped in taxpayers)
            {

                AnsiConsole.MarkupLine(
                    $"[yellow]{categories.FirstOrDefault(x => x.CategoryId == grouped.Key)!.Description}[/]");

                foreach (Taxpayer taxpayer in grouped)
                {
                    Console.WriteLine($"\t{taxpayer.FullName,-20}{taxpayer.StartDate.Value,-12:MM/dd/yyyy}" + 
                                      $"{taxpayer.SocialSecurityNumber}");
                }
            }
        }
    }


}