using ComputerDetails.Classes;

namespace ComputerDetails
{
    internal partial class Program
    {
        static async Task Main(string[] args)
        {
            var canConnect = await LocalDbOperations.CanConnect();
            AnsiConsole.MarkupLine($"    Computer name [cyan1]{Information.ComputerName}[/]");
            AnsiConsole.MarkupLine($"       VPN Status [cyan1]{Information.GetVpnInformation()}[/]");
            AnsiConsole.MarkupLine($"       Home drive [cyan1]{Information.HomeDriveAvailable()}[/]");
            AnsiConsole.MarkupLine($"LocalDb available [cyan1]{canConnect.ToYesNo()}[/]");

            Console.WriteLine();

            var (success, list, _) = await PowerShellOperations.DotNetVersionsInstalledTask();

            if (success)
            {
                AnsiConsole.MarkupLine($"  dotnet versions [cyan1]{string.Join(",", list)}[/]");
            }
            else
            {
                AnsiConsole.MarkupLine("[red]Failed to get dotnet versions[/]");
            }

            var (success1, runTimes, localException) = await PowerShellOperations.DotNetRuntimes();

            AnsiConsole.MarkupLine("dotnet run times installed");
            if (success1)
            {
                Console.WriteLine(runTimes);
            }
            else
            {
                AnsiConsole.MarkupLine("[red]Failed to get dotnet run times[/]");
            }

            Console.ReadLine(); // this line gets removed before installing
        }
    }
}