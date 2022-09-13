
namespace TaxpayerLibrary.Models;

public class Taxpayer
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string FullName => $"{FirstName} {LastName}";
    // ReSharper disable once InconsistentNaming
    public string SSN { get; set; }
    public string Pin { get; set; }
    public DateOnly? StartDate { get; set; }

    public override string ToString() => $"{FirstName} {LastName}";


}