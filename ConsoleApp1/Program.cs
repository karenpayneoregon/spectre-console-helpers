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
            DataOperations.ReadTaxpayersLastNameStartsWithRight();
            //GroupByExample();

            AnsiConsole.MarkupLine("[white on blue]Press a key to exit[/]");
            Console.WriteLine();
            Console.ReadLine();
        }


    }


}