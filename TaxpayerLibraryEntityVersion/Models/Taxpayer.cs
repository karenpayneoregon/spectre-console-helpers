﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace TaxpayerLibraryEntityVersion.Models
{
    public partial class Taxpayer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public string SSN { get; set; }
        public string Pin { get; set; }
        public DateTime? StartDate { get; set; }
    }
}