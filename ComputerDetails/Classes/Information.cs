using System.Diagnostics;
using System.Net.NetworkInformation;

using static System.Environment;
using Version = System.Version;

namespace ComputerDetails.Classes;

/// <summary>
/// Get specific details for a user's environment
/// </summary>
/// <remarks>
/// Any runtime exceptions are not reported on purpose
/// </remarks>
internal class Information
{
    public static string ComputerName => MachineName;
    public static string HomeDriveAvailable()
    {
        try
        {
            return Directory.Exists(GetEnvironmentVariable("HOMESHARE")) ? "Available" : "Unavailable";
        }
        catch (Exception)
        {
            return "Failed to retrieve"; // ignore any errors on purpose
        }
    }

    public static bool IsKarenPayne => UserName == "PayneK";
    
    /// <summary>
    /// Gets the name of the VPN for the organization.
    /// </summary>
    /// <value>
    /// The name of the VPN used for the connection.
    /// </value>
    public static string VpnName => "TODO";
    /// <summary>
    /// For Karen Payne only
    /// </summary>
    public static void Paths()
    {
        if (IsKarenPayne == false) { return; }

        Console.WriteLine();

        HashSet<string> hashSet = new();
        foreach (var path in GetEnvironmentVariable("Path")!.Split(";"))
        {
            hashSet.Add(path);
        }

        var result = hashSet
            .Where(current => current.StartsWith(@"C:\Users\paynek", StringComparison.OrdinalIgnoreCase))
            .ToArray();

        foreach (var item in result)
        {
            Console.WriteLine(item);
        }

        Console.WriteLine(GetEnvironmentVariable("NuGet"));

    }
    public static string GetVpnInformation()
    {
        string status = "VPN not initialize or undetectable";

        try
        {
            var vpn = NetworkInterface
                .GetAllNetworkInterfaces().FirstOrDefault(x => x.Name == VpnName);

            if (vpn is not null)
            {
                status = vpn.OperationalStatus == OperationalStatus.Up ?
                    $"Up and running" :
                    $"VPN is {vpn.OperationalStatus}";

            }

        }
        catch (Exception)
        {
            return "Failed to find the adapter";
        }

        return status;

    }

    public static (VisualStudioInformation info, Exception localException) DevEnvDetails()
    {
        VisualStudioInformation information = new() { ProductVersion = new Version() };
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
            return (information, exception);

        }
    }

}