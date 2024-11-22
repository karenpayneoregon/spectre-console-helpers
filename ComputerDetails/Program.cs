using ComputerDetails.Classes;

namespace ComputerDetails
{
    internal partial class Program
    {
        static async Task Main(string[] args)
        {
            var canConnect = await LocalDbOperations.CanConnect();

            AnsiConsole.MarkupLine($"         [cyan]Computer name[/] {Information.ComputerName}");
            //AnsiConsole.MarkupLine($"LocalDb available [cyan1]{canConnect.ToYesNo()}[/]");

            if (Directory.Exists(@"C:\OED"))
            {
                AnsiConsole.MarkupLine($"            [cyan]VPN Status[/] {Information.GetVpnInformation()}");
                //AnsiConsole.MarkupLine($"       [cyan]Home drive[/] {Information.HomeDriveAvailable()}");
                
            }

            AnsiConsole.MarkupLine($"         [cyan]Visual Studio[/] {Information.DevEnvDetails()}");

            var (success, list, _) = await PowerShellOperations.DotNetVersionsInstalledTask();

            AnsiConsole.MarkupLine(success
                ? $"       [cyan]dotnet versions[/] {string.Join(",", list)}"
                : "[red]Failed to get dotnet versions[/]");

            if (Question("Show .NET Runtimes"))
            {
                await GetDotNetRuntimes();
            }

        }

        private static async Task GetDotNetRuntimes()
        {
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
        }
    }
}