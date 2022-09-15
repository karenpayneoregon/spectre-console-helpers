#nullable disable
using System;
using System.Collections.Generic;

namespace TaxpayerLibraryEntityVersion.Models;

public partial class Taxpayer
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string FullName => $"{FirstName} {LastName}";
    public string SSN { get; set; }
    public string SocialSecurityNumber => SSN.Insert(5, "-").Insert(3, "-");
    public string Pin { get; set; }
    public DateTime? StartDate { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; }
    public override string ToString() => $"{FirstName} {LastName}";
}