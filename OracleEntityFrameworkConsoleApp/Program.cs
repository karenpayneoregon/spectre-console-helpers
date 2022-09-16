using Microsoft.EntityFrameworkCore;
using OracleEntityFrameworkConsoleApp.Classes;
using OracleEntityFrameworkConsoleApp.Data;

namespace OracleEntityFrameworkConsoleApp
{
    internal partial class Program
    {
        static async Task Main(string[] args)
        {
            await Task.Delay(0);
            //await LineFlagsExample();
            await FederalReserveFindExample();

            ExitPrompt();
        }

        private static async Task FederalReserveFindExample()
        {
            try
            {
                string bankName = "ADVIA CREDIT UNION";
                DateTime revised = new(2019, 7, 15);

                await using var context = new OedContext();
                var result = await context.FederalReserveRouting
                    .FirstOrDefaultAsync(x => 
                        x.BankName == bankName && x.LastRevisionDate == revised);

                if (result is not null)
                {
                    AnsiConsole.MarkupLine($"[cyan]Route number[/]: [yellow]{result.RoutingNumber}[/]");
                }
                else
                {
                    AnsiConsole.MarkupLine("[white on red]Record not found[/]");
                }
            }
            catch (Exception localException)
            {
                ExceptionHelpers.ColorStandard(localException);
                AnsiConsole.MarkupLine("[white on blue]Press a key to exit.[/]");
            }
        }

        private static async Task LineFlagsExample()
        {
            try
            {
                int count = 20;
                await using var context = new OedContext();
                var results = await context.LineFlagCodes.Take(count).ToListAsync();

                var table = CreateLineFlagTable(count);
                foreach (var result in results)
                {
                    table.AddRow(
                        result.Code,
                        result.Description.Replace("SCHOOL", "[yellow]SCHOOL[/]"),
                        result.ValidFlag);
                }

                AnsiConsole.Write(table);
            }
            catch (Exception localException)
            {
                ExceptionHelpers.ColorStandard(localException);
                AnsiConsole.MarkupLine("[white on blue]Press a key to exit.[/]");
            }
        }
    }
}