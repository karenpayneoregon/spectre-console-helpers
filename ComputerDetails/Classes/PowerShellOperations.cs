using System.Diagnostics;
using System.Text;
using System.Text.Json;


namespace ComputerDetails.Classes
{
    public class PowerShellOperations
    {
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
    }

}
