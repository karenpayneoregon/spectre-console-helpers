
using ConfigurationLibrary.Classes;
using Microsoft.Data.SqlClient;

namespace ComputerDetails.Classes
{
    internal class LocalDbOperations
    {
        public static async Task<bool> CanConnect()
        {
            try
            {
                await using var cn = new SqlConnection(ConfigurationHelper.ConnectionString());
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
    }
}
