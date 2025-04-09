namespace ComputerDetails.Models;

/// <summary>
/// Represents catalog information associated with a product, including its display version and parsed version details.
/// </summary>
public class CatalogInfo
{
    public string ProductDisplayVersion { get; set; }

    public Version ProductVersion => Version.TryParse(ProductDisplayVersion, out var version) ? version : new Version(0, 0);
}