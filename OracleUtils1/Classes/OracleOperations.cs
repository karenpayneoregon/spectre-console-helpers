using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConfigurationLibrary.Classes;
using Newtonsoft.Json;
using Oracle.ManagedDataAccess.Client;
using OracleUtils1.Models;

namespace OracleUtils1.Classes
{
    internal class OracleOperations
    {
        public static List<OracleSchema> GetTableSchema(string tableName)
        {
            using var cn = new OracleConnection(ConfigurationHelper.ConnectionString());
            using var cmd = new OracleCommand($"SELECT * FROM {tableName}", cn);
            cn.Open();
            OracleDataReader reader = cmd.ExecuteReader();

            DataTable schemaTable = reader.GetSchemaTable()!;
            string json = JsonConvert.SerializeObject(schemaTable, Formatting.Indented);
            File.WriteAllText($"JsonFiles\\{tableName}.json", json);
            return JsonConvert.DeserializeObject<List<OracleSchema>>(json,
                    new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

        }

        public static (List<string> list, Exception exception)  TableNames(string pOwner = "OCS")
        {

            var tableNames = new List<string>();
            var selectStatement = $"SELECT table_name FROM all_tables WHERE OWNER = '{pOwner}' ORDER BY table_name";

            using var cn = new OracleConnection() { ConnectionString = ConfigurationHelper.ConnectionString() };
            using var cmd = new OracleCommand() { Connection = cn };

            cmd.CommandText = selectStatement;

            try
            {

                cn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        tableNames.Add(reader.GetString(0));
                    }
                }
            }
            catch (Exception ex)
            {
                return (tableNames, ex);
            }

            return (tableNames, null);
        }
    }
}
