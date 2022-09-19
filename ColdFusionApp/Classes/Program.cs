using System.Runtime.CompilerServices;
using ConsoleHelperLibrary.Classes;
using Spectre.Console;

// ReSharper disable once CheckNamespace
namespace ColdFusionApp
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
    }
}
