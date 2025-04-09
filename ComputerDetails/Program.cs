using ComputerDetails.Classes;

namespace ComputerDetails
{
    internal partial class Program
    {
        static async Task Main(string[] args)
        {

            AnsiConsole.MarkupLine($"         [cyan]Computer name[/] {Information.ComputerName}");

            if (Directory.Exists(@"C:\OED"))
            {
                AnsiConsole.MarkupLine($"            [cyan]VPN Status[/] {Information.IsVpnConnected().connected.IsConnected()}");
            }

            var current = Studio.Details();
            AnsiConsole.MarkupLine($"         [cyan]Visual Studio[/] {current.DisplayName} - {current.Catalog.ProductDisplayVersion}");

            var (success, list, _) = await PowerShellOperations.DotNetVersionsInstalledTask();
            AnsiConsole.MarkupLine(success
                ? $"       [cyan]dotnet versions[/] {string.Join(",", list)}"
                : "[red]Failed to get dotnet versions[/]");

            if (Question("Show .NET Runtimes"))
            {
                await PowerShellOperations.GetDotNetRuntimes();
            }

        }
    }
}