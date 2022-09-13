using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

// ReSharper disable once CheckNamespace
namespace TaxpayerConsoleApp
{
    internal partial class Program
    {
        [ModuleInitializer]
        public static void Init()
        {
            Console.Title = "Using NuGet packages";
            WindowUtility.SetConsoleWindowPosition(WindowUtility.AnchorWindow.Center);

            AnsiConsole.Write(
                new Panel(new Text("Fictitious data entry", new Style(Color.Magenta1))
                        .Centered())
                    .Expand()
                    .SquareBorder()
                    .BorderStyle(new Style(Color.Chartreuse1))
                    .Header("[LightGreen]OED[/]")
                    .HeaderAlignment(Justify.Center));
        }
    }
}
