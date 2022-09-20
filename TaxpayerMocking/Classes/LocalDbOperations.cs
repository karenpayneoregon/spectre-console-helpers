using System.Data;
using ConfigurationLibrary.Classes;
using Microsoft.Data.SqlClient;

namespace TaxpayerMocking.Classes
{
    internal class LocalDbOperations
    {
        /// <summary>
        /// Determine if our database exists
        /// </summary>
        public static async Task<bool> CanConnect()
        {
			try
            {
                /*
                 * Used to get the initial catalog from the connection string from appsettings.json
                 * which is used as our command parameter which is better hard-coding as the initial
                 * catalog can/may change.
                 */
                var builder = new SqlConnectionStringBuilder(ConfigurationHelper.ConnectionString());
                
				await using var cn = new SqlConnection(ConfigurationHelper.ConnectionString());
                await using var cmd = new SqlCommand()
                {
                    Connection = cn, 
                    CommandText = "SELECT [name] FROM sysdatabases WHERE [name] = @DatabaseName"
                };

                cmd.Parameters.Add("@DatabaseName", SqlDbType.NVarChar).Value = builder.InitialCatalog;
                await cn.OpenAsync();
                var reader = await cmd.ExecuteReaderAsync();
                return reader.HasRows;
            }
			catch (Exception)
            {
                return false;
            }

        }
    }
}
