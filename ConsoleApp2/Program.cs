using Microsoft.Win32;
using System.Diagnostics;
using System.Net.NetworkInformation;
#pragma warning disable CS8619 // Nullability of reference types in value doesn't match target type.
#pragma warning disable CA1416

namespace ConsoleApp2;

internal class Program
{
    static void Main(string[] args)
    {
        //var list = NetworkInterface.GetAllNetworkInterfaces();


        DevEnvDetails();
        Console.ReadLine();
    }

    public static (VisualStudioInformation info, Exception localException) DevEnvDetails()
    {
        VisualStudioInformation information = new() {ProductVersion = new Version()};
        try
        {
            var item = Process.GetProcessesByName("DevEnv")[0].Modules[0].FileVersionInfo;
            information.FileVersion = item.FileVersion;
            information.FileDescription = item.FileDescription;

            if (Version.TryParse(item.ProductVersion, out var pv))
            {
                information.ProductVersion = pv;
            }
            return (information, null);
        }
        catch (Exception exception)
        {
            return (new VisualStudioInformation() {ProductVersion = new Version()}, exception);

        }
    }

    private static bool GetInstalledVsVersions()
    {
        string registryPath = @"SOFTWARE\Wow6432Node\Microsoft\DevDiv\vs\Servicing";
        try
        {
            // Open the registry key
            using (RegistryKey key = Registry.LocalMachine.OpenSubKey(registryPath))
            {
                if (key == null)
                {
                    Console.WriteLine("Registry path not found.");
                    return true;
                }

                string[] subKeyNames = key.GetSubKeyNames();

                if (subKeyNames.Length > 0)
                {
                    Console.WriteLine("Installed Visual Studio Versions:");
                    foreach (string version in subKeyNames)
                    {
                        Console.WriteLine($"- Version: {version}");
                    }
                }
                else
                {
                    Console.WriteLine("No Visual Studio versions found.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error reading registry: {ex.Message}");
        }

        return false;
    }
}

public class VisualStudioInformation
{
    public string? FileVersion { get; set; }
    public required Version ProductVersion { get; set; }
    public string? FileDescription { get; set; }
}
