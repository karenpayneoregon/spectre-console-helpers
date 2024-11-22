
#nullable disable
namespace ComputerDetails.Classes;
public class VisualStudioInformation
{
    public string FileVersion { get; set; }
    public Version ProductVersion { get; set; }
    public string FileDescription { get; set; }
    public override string ToString() => ProductVersion.ToString();

}