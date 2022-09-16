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

        //EF.Taxpayer taxpayer = await AddTaxpayerEntityVersion();
        //await EditTaxpayerDataProvider();
        //await EntityDataOperations.GetOriginalValuesAfterEditingVersion2();
        await ReadTaxpayersUsingDataProvider();
        Console.ReadLine();
    }

    private static async Task EditTaxpayerEntityFrameworkCore()
    {
        var (taxpayer, success) = await EntityDataOperations.EditTaxpayer();
        if (success)
        {
            if (taxpayer.StartDate.HasValue)
            {
                Console.WriteLine(taxpayer.StartDate.Value.ToString("MM/dd/yyyy"));
            }
        }
    }

    /// <summary>
    /// using SqlClient data provider
    /// Read all taxpayers
    /// </summary>
    private static async Task ReadTaxpayersUsingDataProvider()
    {
        List<Taxpayer> list = await DataOperations.GetTaxpayers();

        foreach (var taxpayer in list)
        {
            AnsiConsole.MarkupLine($"{taxpayer.Id,-3}{taxpayer.FullName,-23} {taxpayer.StartDate?.ToString("MM/dd/yyyy")} {taxpayer.SSN} {taxpayer.Pin} {taxpayer.CategoryId}");
        }
    }

    /// <summary>
    /// using SqlClient data provider
    /// Read one taxpayer by primary key
    /// </summary>
    private static async Task ReadOneTaxpayerUsingDataProvider()
    {
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
    }

    /// <summary>
    /// using EF Core
    /// Read all taxpayers
    /// </summary>
    private static async Task ReadTaxpayersUsingEntityFrameworkCore()
    {
        List<EF.Taxpayer> list = await EntityDataOperations.GetTaxpayers();

        foreach (var taxpayer in list)
        {
            AnsiConsole.MarkupLine($"{taxpayer.Id,-3}{taxpayer.FullName} {taxpayer.StartDate} {taxpayer.SSN} {taxpayer.Pin}");
        }
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

    private static async Task EditTaxpayerDataProvider()
    {
        var (taxpayer, success) = await DataOperations.EditTaxpayer();
        if (success)
        {
            if (taxpayer.StartDate.HasValue)
            {
                Console.WriteLine(taxpayer.StartDate.Value);
            }
        }
        else
        {
            Console.WriteLine("Taxpayer not found");
        }
    }


    /// <summary>
    /// using SqlClient data provider
    /// Add new taxpayer, on returning from the add operation
    /// the taxpayer var will have the new primary key
    /// </summary>
    public static async Task<Taxpayer> AddTaxpayer()
    {
        Taxpayer taxpayer = new Taxpayer()
        {
            FirstName = "Barry", 
            LastName = "Brown", 
            SSN = "598910366", 
            Pin = "4354", 
            StartDate = new DateOnly(2022,9,1)
        };

        await DataOperations.AddNewTaxpayer(taxpayer);
        
        return taxpayer;
    }


    /// <summary>
    /// using EF Core
    /// Add new taxpayer, on returning from the add operation
    /// the taxpayer var will have the new primary key
    /// </summary>
    public static async Task<EF.Taxpayer> AddTaxpayerEntityVersion()
    {
        EF.Taxpayer taxpayer = new ()
        {
            FirstName = "Barry",
            LastName = "Brown",
            SSN = "598910366",
            Pin = "4354",
            StartDate = new DateTime(2022,9,1)
        };

        await EntityDataOperations.AddNewTaxpayer(taxpayer);

        return taxpayer;
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
}