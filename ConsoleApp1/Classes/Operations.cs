using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1.Models;
using Newtonsoft.Json;

namespace ConsoleApp1.Classes;

internal class Operations
{
    public static string TaxpayerFileName = "Taxpayers.json";
    public static string CategoryFileName = "Categories.json";

    /// <summary>
    /// Read <see cref="Taxpayer"/> from json to a list
    /// </summary>
    public static List<Taxpayer> ReadTaxpayers() 
        => JsonConvert.DeserializeObject<List<Taxpayer>>(File.ReadAllText(TaxpayerFileName));

    /// <summary>
    /// Read all <see cref="Category"/> into a list from json
    /// </summary>
    public static List<Category> ReadCategories()
        => JsonConvert.DeserializeObject<List<Category>>(File.ReadAllText(CategoryFileName));
}