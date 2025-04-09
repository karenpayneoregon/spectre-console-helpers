using System.Diagnostics;
using System.Text;
using System.Text.Json;


namespace ComputerDetails.Classes;

public class PowerShellOperations
{
    /// <summary>
    /// Asynchronously retrieves a list of installed .NET SDK versions by executing a PowerShell command.
    /// </summary>
    /// <returns>
    /// A tuple containing the following:
    /// <list type="bullet">
    /// <item>
    /// <description>A <see cref="bool"/> indicating whether the operation was successful.</description>
    /// </item>
    /// <item>
    /// <description>A <see cref="List{Version}"/> containing the parsed .NET SDK versions if successful, or an empty list if not.</description>
    /// </item>
    /// <item>
    /// <description>An <see cref="Exception"/> instance representing any error that occurred during the operation, or <c>null</c> if no error occurred.</description>
    /// </item>
    /// </list>
    /// </returns>
    /// <remarks>
    /// This method uses PowerShell to locate and parse installed .NET SDK versions. It executes a command to retrieve the SDK directories,
    /// converts the output to JSON, and attempts to parse each directory name as a <see cref="Version"/>.
    /// </remarks>
    /// <exception cref="InvalidOperationException">Thrown if the PowerShell process fails to start or execute correctly.</exception>
    /// <exception cref="JsonException">Thrown if the JSON output from the PowerShell command cannot be deserialized.</exception>
    /// <example>
    /// <code>
    /// var (success, versions, exception) = await PowerShellOperations.DotNetVersionsInstalledTask();
    /// if (success)
    /// {
    ///     Console.WriteLine($"Installed .NET SDK versions: {string.Join(", ", versions)}");
    /// }
    /// else
    /// {
    ///     Console.WriteLine($"Failed to retrieve .NET SDK versions: {exception.Message}");
    /// }
    /// </code>
    /// </example>
    public static async Task<(bool succcess, List<Version> list, Exception localException)> DotNetVersionsInstalledTask()
    {

        List<Version> list = new();
        try
        {

            var start = new ProcessStartInfo
            {
                FileName = "powershell.exe",
                RedirectStandardOutput = true,
                Arguments = "(dir (Get-Command dotnet).Path.Replace('dotnet.exe', 'sdk')).Name | ConvertTo-Json",
                CreateNoWindow = true
            };

            using var process = Process.Start(start);
            using var reader = process!.StandardOutput;

            process.EnableRaisingEvents = true;
            var info = JsonSerializer.Deserialize<string[]>(await reader.ReadToEndAsync());

            foreach (var item in info)
            {
                if (Version.TryParse(item, out var result))
                {
                    list.Add(result);
                }
            }

            return (true, list, null);
        }
        catch (Exception localException)
        {

            return (false, list, localException);
        }
    }

    /// <summary>
    /// Executes a PowerShell command to retrieve the list of installed .NET runtimes on the system.
    /// </summary>
    /// <returns>
    /// A task that represents the asynchronous operation. The result is a tuple containing:
    /// <list type="bullet">
    /// <item>
    /// <description>A <see cref="bool"/> indicating whether the operation was successful.</description>
    /// </item>
    /// <item>
    /// <description>A <see cref="string"/> containing the formatted list of installed .NET runtimes, or "Failed" if the operation was unsuccessful.</description>
    /// </item>
    /// <item>
    /// <description>An <see cref="Exception"/> object representing any exception that occurred during the operation, or <c>null</c> if no exception occurred.</description>
    /// </item>
    /// </list>
    /// </returns>
    /// <remarks>
    /// This method uses PowerShell to execute the <c>dotnet --list-runtimes</c> command and processes the output.
    /// </remarks>
    public static async Task<(bool succcess, string, Exception localException)> DotNetRuntimes()
    {
        try
        {

            var start = new ProcessStartInfo
            {
                FileName = "powershell.exe",
                RedirectStandardOutput = true,
                Arguments = "dotnet --list-runtimes",
                CreateNoWindow = true
            };

            using var process = Process.Start(start);
            using var reader = process!.StandardOutput;

            process.EnableRaisingEvents = true;

            var lineData = await reader.ReadToEndAsync();
            var items = lineData.Split([Environment.NewLine], StringSplitOptions.RemoveEmptyEntries);

            StringBuilder builder = new();
            foreach (var item in items)
            {
                if (item.Contains('['))
                {
                    builder.AppendLine("   " + item[..(item.IndexOf("[", StringComparison.Ordinal) - 1)]);
                }
            }

            return (true, builder.ToString(), null);
        }
        catch (Exception localException)
        {

            return (false, "Failed", localException);
        }
    }

    /// <summary>
    /// Asynchronously retrieves and displays the list of installed .NET runtimes on the system.
    /// </summary>
    /// <returns>
    /// A task that represents the asynchronous operation. Upon completion, the method displays the list of installed .NET runtimes
    /// if the operation is successful, or an error message if it fails.
    /// </returns>
    /// <remarks>
    /// This method internally calls <see cref="DotNetRuntimes"/> to execute the PowerShell command <c>dotnet --list-runtimes</c>.
    /// The output is processed and displayed using <c>AnsiConsole</c>.
    /// </remarks>
    public static async Task GetDotNetRuntimes()
    {
        var (success1, runTimes, localException) = await DotNetRuntimes();

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