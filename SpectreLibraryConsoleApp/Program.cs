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
        /// <summary>
        /// Demonstrates working with <see cref="DateOnly"/> and <see cref="TimeOnly"/>
        /// input
        /// </summary>
        private static void GetInoutExamples()
        {
            var password = Prompts.GetNewPassword("Enter a new password");
            Console.WriteLine(password);

            TimeOnly? only = Prompts.GetTimeOnly($"{Now:HH}:00");
            if (only is not null)
            {
                Console.WriteLine(only);
            }
            else
            {
                Console.WriteLine("Null");
            }

            DateOnly? dateOnly = Prompts.GetDateOnly(new DateOnly(2022, 2, 2));
            Console.WriteLine(dateOnly is not null ? dateOnly.Value.ToString("MM/dd/yyyy") : "Null");


            Console.WriteLine(Prompts.GetBool("Question").ToYesNo());
            Console.WriteLine(Prompt.GetYesNo("Question", true).ToYesNo());
  
        }
    }
}