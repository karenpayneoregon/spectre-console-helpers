using ConfigurationLibrary.Classes;
using Newtonsoft.Json;
using Oracle.ManagedDataAccess.Client;
using OracleUtils1.Classes;
using OracleUtils1.Models;
using System.Data;

namespace OracleUtils1;

internal partial class Program
{
    /// <summary>
    /// Get schema for an oracle table
    /// Send to json to a file and also deserialize to a model
    /// </summary>
    static void Main(string[] args)
    {

        var (list, _ ) = OracleOperations.TableNames();
        Random random = new Random();
        int index = random.Next(1, list.Count -1);
        var tableName = list[44];
        var schema = OracleOperations.GetTableSchema(tableName);

        var columns = string.Join(",", schema.Select(x => x.BaseColumnName).ToArray());
        Console.WriteLine($"SELECT {columns} FROM {tableName}");


        Console.ReadLine();
    }
}
