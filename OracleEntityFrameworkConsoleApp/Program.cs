using OracleEntityFrameworkConsoleApp.Classes;
using OracleEntityFrameworkConsoleApp.Data;

namespace OracleEntityFrameworkConsoleApp
{
    internal partial class Program
    {
        static void Main(string[] args)
        {
            try
            {
                int count = 20;
                using var context = new OedContext();
                var results = context.LineFlagCodes.Take(count).ToList();

                var table = CreateTable(count);
                foreach (var result in results)
                {

                    table.AddRow(
                        result.Code,
                        result.Description.Replace("SCHOOL", "[yellow]SCHOOL[/]"),
                        result.Valid_Flag);

                }

                AnsiConsole.Write(table);

            }
            catch (Exception localException)
            {
                ExceptionHelpers.ColorStandard(localException);
                AnsiConsole.MarkupLine("[white on blue]Press a key to exit.[/]");
            }

            ExitPrompt();

            
        }
    }
}