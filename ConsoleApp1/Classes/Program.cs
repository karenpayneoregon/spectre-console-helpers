using System.Runtime.CompilerServices;
using Spectre.Console;
using static ConsoleHelperLibrary.Classes.WindowUtility;

// ReSharper disable once CheckNamespace
namespace ConsoleApp1
{
    internal partial class Program
    {
        [ModuleInitializer]
        public static void Init()
        {
            Console.Title = "Code sample: Getting to know Visual Studio and C#";
            SetConsoleWindowPosition(AnchorWindow.Fill);
        }


    }
}
