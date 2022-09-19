using ColdFusionLibrary.Classes;
using ColdFusionLibrary.Models;
using Spectre.Console;


namespace ColdFusionApp
{
    internal partial class Program
    {
        static async Task Main(string[] args)
        {
            List<FederalReserveRouting>? records = await DataOperation.FederalReserveRoutingAsync();
            var table = CreateTable();
            foreach (var record in records)
            {
                table.AddRow(
                    record.ROUTING_NUM,
                    record.BANK_NAME,
                    record.POST_DATE.ToString("MM/dd/yyyy")
                    );
            }

            AnsiConsole.Write(table);

            Console.WriteLine();

            AnsiConsole.MarkupLine("[white on blue]Done[/]");

            Console.ReadLine();
        }
        private static Table CreateTable()
        {
            return new Table()
                .RoundedBorder().BorderColor(Color.LightSlateGrey)
                .AddColumn("[b]Route[/]")
                .AddColumn("[b]Bank[/]")
                .AddColumn("[b]Post date[/]")
                .Alignment(Justify.Center)
                .Title("[white on chartreuse3]Records[/]");
        }
    }
}