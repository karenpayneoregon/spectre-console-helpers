using SpectreConsoleLibrary;
using TaxpayerLibrary.Classes;
using TaxpayerLibrary.Models;
using TaxpayerLibraryEntityVersion.Classes;

namespace TaxpayerConsoleApp;
/*
 * Since there are two Taxpayer classes we must alias
 * one of them as they are not equal even though they have the same
 * properties.
 */
using EF = TaxpayerLibraryEntityVersion.Models;
internal partial class Program
{
    static async Task Main(string[] args)
    {
        await Task.Delay(0);

        int id = 1;
        var (taxpayer, found) = await DataOperations.GetTaxpayer(id);
        if (found)
        {
            Console.WriteLine(taxpayer.FullName);
        }
        else
        {
            Console.WriteLine("Not found");
        }
        Console.ReadLine();
    }

    private static async Task ReadTaxpayersUsingEntityFrameworkCore()
    {
        List<EF.Taxpayer> list = await EntityDataOperations.GetTaxpayers();

        foreach (var taxpayer in list)
        {
            AnsiConsole.MarkupLine($"{taxpayer.Id,-3}{taxpayer.FullName} {taxpayer.StartDate} {taxpayer.SSN} {taxpayer.Pin}");
        }
    }

    private static async Task ReadTaxpayersUsingDataProvider()
    {
        List<Taxpayer> list = await DataOperations.GetTaxpayers();

        foreach (var taxpayer in list)
        {
            AnsiConsole.MarkupLine($"{taxpayer.Id,-3}{taxpayer.FullName} {taxpayer.StartDate} {taxpayer.SSN} {taxpayer.Pin}");
        }
    }

    private static void PromptForTaxpayer()
    {
        var taxpayer = GetTaxpayer();
        Console.Clear();

        AnsiConsole.MarkupLine("[cyan]Taxpayer details[/]");
        AnsiConsole.MarkupLine($"\t[yellow]Name[/] {taxpayer.FirstName} {taxpayer.LastName}");
        AnsiConsole.MarkupLine($"\t[yellow]SSN[/] {taxpayer.SSN}");
        AnsiConsole.MarkupLine($"\t[yellow]Pin[/] {taxpayer.Pin}");
        AnsiConsole.MarkupLine($"\t[yellow]Start date[/] {taxpayer.StartDate}");

        Console.ReadLine();
    }

    internal static Taxpayer GetTaxpayer()
    {
            
        Taxpayer taxpayer = new()
        {
            FirstName = Prompts.GetFirstName(false),
            LastName = Prompts.GetLastName(false),
            StartDate = Prompts.GetDateOnly(new DateOnly(2022, 2, 2)),
            SSN = Prompts.GetSSN(),
            Pin = Prompts.GetPin().ToString()
        };


        return taxpayer;
    }
}