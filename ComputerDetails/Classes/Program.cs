using System.Runtime.CompilerServices;

// ReSharper disable once CheckNamespace
namespace ComputerDetails
{
    internal partial class Program
    {
        [ModuleInitializer]
        public static void Init()
        {
            AnsiConsole.MarkupLine($"[cyan1]OED Web team[/] (C) {DateTime.Now.Year}");
            AnsiConsole.MarkupLine($"By Karen Payne");
            Console.WriteLine();
        }
    }
}
