namespace ComputerDetails.Models;

/// <summary>
/// Represents an installed instance of Visual Studio on the system.
/// </summary>
/// <remarks>
/// This class encapsulates details about a specific Visual Studio installation, 
/// including its display name, installation path, and associated catalog information.
/// </remarks>
public class VisualStudioInstance
{
    public string DisplayName { get; set; }
    public string InstallationPath { get; set; }
    public CatalogInfo Catalog { get; set; }
}