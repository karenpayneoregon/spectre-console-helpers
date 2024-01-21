using SpectreConsoleLibrary;
using SpectreLibraryConsoleApp.Classes;
using static System.DateTime;

namespace SpectreLibraryConsoleApp
{
    internal partial class Program
    {
        static void Main(string[] args)
        {
            GenericSelectionListExample();
            Console.ReadLine();
        }

        /// <summary>
        /// Demonstrates creating a multi-select menu using generics
        /// </summary>
        private static void GenericSelectionListExample()
        {
            var companies = Prompts.GenericSelectionList(
                BogusOperations.Companies(), 10, "Select");

            if (companies.Any())
            {
                var table = new Table()
                    .RoundedBorder()
                    .AddColumn("[b]Id[/]")
                    .AddColumn("[b]Name[/]")
                    .Alignment(Justify.Left)
                    .BorderColor(Color.LightSlateGrey)
                    .Title("[LightGreen]Choices[/]");

                foreach (var company in companies)
                {
                    table.AddRow(company.Id.ToString(), company.Name);
                }

                AnsiConsole.Write(table);
            }
            else
            {
                Console.WriteLine("No companies selected");
            }
        }

    }
}