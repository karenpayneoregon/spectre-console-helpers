using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConfigurationLibrary.Classes;
using TaxpayerLibrary.Models;

namespace TaxpayerLibrary.Classes
{
    public class DataOperations
    {
        public static async Task<List<Taxpayer>> GetTaxpayers()
        {

            List<Taxpayer> list = new();


            var statement = "SELECT Id,FirstName,LastName,SSN,Pin,StartDate FROM dbo.Taxpayer";
            await using var cn = new SqlConnection(ConfigurationHelper.ConnectionString());
            await using var cmd = new SqlCommand { Connection = cn, CommandText = statement };

            await cn.OpenAsync();
            await using var reader = await cmd.ExecuteReaderAsync();

            while (reader.Read())
            {
                list.Add(new Taxpayer()
                {
                    Id = reader.GetInt32(0), 
                    FirstName = reader.GetString(1), 
                    LastName = reader.GetString(2), 
                    SSN = reader.GetString(3), 
                    Pin = reader.GetString(4), 
                    StartDate = DateOnly.FromDateTime(reader.GetDateTime(5))
                });
            }

            return list;
        }
    }
}
