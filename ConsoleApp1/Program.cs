using System.Security.Cryptography.X509Certificates;
using ConsoleApp1.Classes;
using ConsoleApp1.Models;
using Spectre.Console;

namespace ConsoleApp1;

internal partial class Program
{
    static async Task Main()
    {
        var timer = new PeriodicTimer(TimeSpan.FromSeconds(1));
        int counter = 1;
        while (await timer.WaitForNextTickAsync() && counter < 3)
        {
            counter++;

        }
        DataOperations.ReadTaxpayersLastNameStartsWithRight();
        //GroupByExample();
        //var low = new List<int> { 1, 2, 3, 4, 11 }.Select(IntExtensions.Ranking);
        //var high = new[] { 1_001, 1_002, 1_003, 1_004, 1_011 }.Select(x => x.Ranking());
        //Console.WriteLine();
        //AnsiConsole.MarkupLine("[white on blue]Press a key to exit[/]");
        //Console.WriteLine($"First collection: {string.Join(", ", low)}");
        //Console.WriteLine($"Second collection: {string.Join(", ", high)}");
        //Console.WriteLine(89.Ranking());

  
        Console.ReadLine();
    }


}


public static class IntExtensions
{
    public static string Ranking(this int value)
    {
        var ones = value % 10;
        var tens = value % 100;

        var suffix = ones switch
        {
            1 when tens != 11 => "st",
            2 when tens != 12 => "nd",
            3 when tens != 13 => "rd",
            _ => "th"
        };

        return string.Concat(value.ToString("N0"), suffix);
    }
}