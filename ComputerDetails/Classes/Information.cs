using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Text.Json;
using static System.Environment;

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
    public static string VpnName => "_OED_OED_SYSADMIN_NA - raoed.ets.oregon.gov";

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
        string status = "OED VPN not initialize or undetectable";

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



}