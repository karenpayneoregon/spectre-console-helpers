using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConfigurationLibrary.Classes;
using TaxpayerLibrary.Models;
using System.Reflection.PortableExecutable;
using System.Diagnostics.CodeAnalysis;
using DbPeekQueryLibrary.LanguageExtensions;
using static System.DateTime;

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

        /// <summary>
        /// Get <see cref="Taxpayer"/> by primary key
        /// </summary>
        /// <param name="id">key to find</param>
        /// <returns>
        /// A Taxpayer if found else null.
        /// Use found (from deconstruct) to determine if the taxpayer was found
        /// </returns>
        public static async Task<(Taxpayer taxpayer, bool found)> GetTaxpayer(int id)
        {
            var statement = 
                "SELECT FirstName,LastName,SSN,Pin,StartDate " +
                "FROM dbo.Taxpayer WHERE Id = @Id";

            await using var cn = new SqlConnection(ConfigurationHelper.ConnectionString());
            await using var cmd = new SqlCommand { Connection = cn, CommandText = statement };
            cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;

            await cn.OpenAsync();
            await using var reader = await cmd.ExecuteReaderAsync();

            if (reader.HasRows)
            {
                reader.Read();
                return (new Taxpayer()
                {
                    Id = id, 
                    FirstName = reader.GetString(0),
                    LastName = reader.GetString(1),
                    SSN = reader.GetString(2),
                    Pin = reader.GetString(3),
                    StartDate = DateOnly.FromDateTime(reader.GetDateTime(4))
                },true);
            }
            else
            {
                return (null, false);
            }
            
        }


        /// <summary>
        /// Same as above but using .NET Framework code style e.g.
        /// The two variables 'cn' and 'cmd' have {} while in the above
        /// we use .NET Core 5 and higher syntax
        /// </summary>
        [SuppressMessage("ReSharper", "ConvertToUsingDeclaration")]
        public static async Task<(Taxpayer, bool)> GetTaxpayerOldSchool(int id)
        {
            var statement =
                "SELECT FirstName,LastName,SSN,Pin,StartDate " +
                "FROM dbo.Taxpayer WHERE Id = @Id";

            
            await using (var cn = new SqlConnection(ConfigurationHelper.ConnectionString()))
            {
                await using (var cmd = new SqlCommand { Connection = cn, CommandText = statement })
                {
                    cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;
                    await cn.OpenAsync();
                    await using var reader = await cmd.ExecuteReaderAsync();
                    if (reader.HasRows)
                    {
                        reader.Read();
                        return (new Taxpayer()
                        {
                            Id = id,
                            FirstName = reader.GetString(0),
                            LastName = reader.GetString(1),
                            SSN = reader.GetString(2),
                            Pin = reader.GetString(3),
                            StartDate = DateOnly.FromDateTime(reader.GetDateTime(4))
                        }, true);
                    }
                    else
                    {
                        return (null, false);
                    }
                }
            }
        }

        public static async Task<(bool success, Exception exception)> AddNewTaxpayer(Taxpayer taxpayer)
        {

            var statement = @"
INSERT INTO [dbo].[Taxpayer]  ([FirstName],[LastName],[SSN],[Pin],[StartDate]) 
VALUES (@FirstName,@LastName,@SSN,@Pin,@StartDate);
SELECT CAST(scope_identity() AS int);
";

            await using var cn = new SqlConnection(ConfigurationHelper.ConnectionString());
            await using var cmd = new SqlCommand { Connection = cn, CommandText = statement };

            cmd.Parameters.Add("@FirstName", SqlDbType.NVarChar).Value = taxpayer.FirstName;
            cmd.Parameters.Add("@LastName", SqlDbType.NVarChar).Value = taxpayer.LastName;
            cmd.Parameters.Add("@SSN", SqlDbType.NVarChar).Value = taxpayer.SSN;
            cmd.Parameters.Add("@Pin", SqlDbType.NVarChar).Value = taxpayer.Pin;
            cmd.Parameters.Add("@StartDate", SqlDbType.Date).Value =  
                taxpayer.StartDate!.Value.ToDateTime(new TimeOnly(0,0,0));


            try
            {
                await cn.OpenAsync();
                taxpayer.Id = Convert.ToInt32(cmd.ExecuteScalar());
                return (true, null);

            }
            catch (Exception localException)
            {
                return (false, localException);
            }
        }

        public static async Task<(Taxpayer taxpayer, bool)> EditTaxpayer()
        {
            int id = 1;
            var newStartDate = "2022-09-14";
            RandomDateTime date = new RandomDateTime();
            var value = date.DateValue(date.Next());
            
            var (taxpayer, found) = await GetTaxpayer(id);
            if (found)
            {
                taxpayer.StartDate = DateOnly.Parse(value);
                await using var cn = new SqlConnection(ConfigurationHelper.ConnectionString());
                string statement = "UPDATE [dbo].[Taxpayer] SET StartDate = @StartDate WHERE Id = @Id";
                await using var cmd = new SqlCommand { Connection = cn, CommandText = statement };
                cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;
                cmd.Parameters.Add("@StartDate", SqlDbType.Date).Value = newStartDate;

                await cn.OpenAsync();
                await cmd.ExecuteNonQueryAsync();

                return (taxpayer, true);

            }
            else
            {
                return (null, false);
            }
        }
    }
}
