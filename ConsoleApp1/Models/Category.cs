﻿#nullable disable
using System.ComponentModel.DataAnnotations;

namespace ConsoleApp1.Models;

public class Category
{
    [Key]
    public int CategoryId { get; set; }
    public string Description { get; set; }
    public List<Taxpayer> Taxpayers { get; set; }

    public Category()
    {
        Taxpayers = new List<Taxpayer>();
    }
}