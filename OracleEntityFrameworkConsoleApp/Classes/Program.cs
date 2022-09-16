using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

// ReSharper disable once CheckNamespace
namespace OracleEntityFrameworkConsoleApp
{
    internal partial class Program
    {
        [ModuleInitializer]
        public static void Init()
        {
            AnsiConsole.MarkupLine("");
            Console.Title = "Code sample";
            WindowUtility.SetConsoleWindowPosition(WindowUtility.AnchorWindow.Center);
        }
        private static Table CreateLineFlagTable(int count)
        {
            return new Table()
                .RoundedBorder().BorderColor(Color.LightSlateGrey)
                .AddColumn("[b]Code[/]")
                .AddColumn("[b]Description[/]")
                .AddColumn("[b]Flag[/]")
                .Alignment(Justify.Center)
                .Title($"[white on blue]First {count} rows in table[/]");
        }


        private static void Render(Rule rule)
        {
            AnsiConsole.Write(rule);
            AnsiConsole.WriteLine();
        }

        private static void ExitPrompt()
        {

            Render(new Rule($"[white on blue]Press a key to exit[/]")
                .RuleStyle(Style.Parse("white"))
                .Centered());

            Console.ReadLine();
        }
    }
}
