using Microsoft.EntityFrameworkCore;
using OracleEntityFrameworkConsoleApp.Classes;
using OracleEntityFrameworkLibrary.Data;
using OracleEntityFrameworkLibrary.Models;


//using TaxpayerLibraryEntityVersion.Data;

namespace OracleEntityFrameworkConsoleApp
{
    internal partial class Program
    {
        static async Task Main(string[] args)
        {
            await Task.Delay(0);
            await LineFlagsExample();
            await FederalReserveFindExample();

            ExitPrompt();
        }

        /// <summary>
        /// An example to read data to return one record based on two conditions.
        /// Once the operation to query a record has completed we assert using
        /// `result is not null` to see if any records matched our two conditions
        /// </summary>
        /// <remarks>
        /// Code is wrapped in a try/catch so that in the event a runtime exception
        /// such as server is not available we don't crash-n-burn
        /// </remarks>
        private static async Task FederalReserveFindExample()
        {
            try
            {
                string bankName = "ADVIA CREDIT UNION";
                DateTime revised = new(2019, 7, 15);

                var result = await DataOperations.FederalReserveRouting(bankName, revised);

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
        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// Code is wrapped in a try/catch so that in the event a runtime exception
        /// such as server is not available we don't crash-n-burn
        /// </remarks>
        private static async Task LineFlagsExample()
        {
            try
            {
                int count = 20;
                List<LineFlagCodes> results = await DataOperations.GetLineFlagCodesList();

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