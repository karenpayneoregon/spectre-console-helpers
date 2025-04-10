using System.Diagnostics;
using System.Net.NetworkInformation;

using static System.Environment;
using Version = System.Version;

namespace ComputerDetails.Classes;

/// <summary>
/// Provides methods and properties to retrieve and manage details about the user's computer environment.
/// </summary>
/// <remarks>
/// This class includes functionality to determine the computer name, check the availability of the home drive, 
/// verify VPN connectivity, and retrieve VPN-related information. It also contains methods specific to certain users.
/// Any runtime exceptions are intentionally not reported.
/// </remarks>
internal class Information
{
    public static string ComputerName => MachineName;

    /// <summary>
    /// Checks the availability of the user's home drive.
    /// </summary>
    /// <returns>
    /// A string indicating the availability of the home drive:
    /// - "Available" if the home drive exists.
    /// - "Unavailable" if the home drive does not exist.
    /// - "Failed to retrieve" if an error occurs during the check.
    /// </returns>
    /// <remarks>
    /// This method retrieves the home drive path from the "HOMESHARE" environment variable
    /// and checks if the directory exists. Any exceptions encountered during execution
    /// are intentionally ignored, and a failure message is returned instead.
    /// </remarks>
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

    // change to your username
    public static bool IsKarenPayne => UserName == "PayneK";
    
    /// <summary>
    /// Gets the name of the VPN for the organization.
    /// </summary>
    /// <value>
    /// The name of the VPN used for the connection.
    /// </value>
    public static string VpnName => "TODO";
    /// <summary>
    /// Displays and filters environment paths specific to the user "Karen Payne".
    /// </summary>
    /// <remarks>
    /// This method retrieves the "Path" environment variable, filters the paths that start with 
    /// "C:\Users\paynek" (case-insensitive), and displays them. Additionally, it outputs the value 
    /// of the "NuGet" environment variable. The method only executes if the current user is "Karen Payne".
    /// </remarks>
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

    /// <summary>
    /// Determines whether a VPN connection is currently active and retrieves the VPN's name.
    /// </summary>
    /// <returns>
    /// A tuple containing two elements:
    /// - <see cref="bool"/>: Indicates whether a VPN connection is active.
    /// - <see cref="string"/>: The name of the VPN if connected, or an error message if not connected or undetectable.
    /// </returns>
    /// <remarks>
    /// This method iterates through all network interfaces to identify any that are operational and of type PPP,
    /// which commonly indicates a VPN connection. Additional checks are performed on the interface name and description
    /// to confirm VPN-related keywords. If an exception occurs during execution, it returns a failure message.
    /// </remarks>
    public static (bool connected, string vpnName) IsVpnConnected()
    {
        try
        {
            foreach (var ni in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (ni is { OperationalStatus: OperationalStatus.Up, NetworkInterfaceType: NetworkInterfaceType.Ppp }) // PPP often indicates VPN
                {
                    // Additional check: sometimes VPN interfaces have names like "VPN", "Secure", etc.
                    string name = ni.Name.ToLower();
                    string description = ni.Description.ToLower();
                    if (name.Contains("vpn") || name.Contains("_common_") || description.Contains("vpn"))
                    {
                        return (true, ni.Name);
                    }
                }
            }

            return (false,"VPN not initialize or undetectable");
        }
        catch (Exception e)
        {
            return (false, $"Failed to find the adapter {e.Message}");
        }
    }

    /// <summary>
    /// Retrieves information about the current VPN connection status.
    /// </summary>
    /// <returns>
    /// A string describing the VPN connection status:
    /// - "VPN not initialized or undetectable" if no VPN adapter is found.
    /// - "Up and running" if the VPN adapter is operational.
    /// - "VPN is [OperationalStatus]" if the VPN adapter is found but not operational.
    /// - "Failed to find the adapter" if an exception occurs during the retrieval process.
    /// </returns>
    /// <remarks>
    /// This method searches for a network interface matching the specified VPN name and checks its operational status.
    /// If the VPN adapter is found, its status is returned. If no adapter is found or an exception occurs, an appropriate
    /// message is returned instead.
    /// </remarks>
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
}