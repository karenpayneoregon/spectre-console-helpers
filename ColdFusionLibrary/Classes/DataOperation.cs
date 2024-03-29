﻿using System.Data;
using ColdFusionLibrary.LanguageExtensions;
using ColdFusionLibrary.Models;
using Oracle.ManagedDataAccess.Client;
using static ConfigurationLibrary.Classes.ConfigurationHelper;

namespace ColdFusionLibrary.Classes
{
    public class DataOperation
    {
        /// <summary>
        /// Mockup of a MPC method for SIDES
        /// </summary>
        /// <remarks>
        /// * This would be better done using EF Core
        /// * A DataTable is used so Karen could bypass using a reader
        ///   and is not recommended but Karen did not want to type out what
        ///   it takes to while reader.Read etc
        /// </remarks>
        public static async Task<List<FederalReserveRouting>> FederalReserveRoutingAsync()
        {

            await using OracleConnection cn =  new () { ConnectionString = ConnectionString() };
            await using OracleCommand cmd = new()
            {
                Connection = cn,
                CommandText = SqlStatements.FederalReserveRouting()
            };


            cn.Open();

            DataTable dt = new();

            dt.Load(await cmd.ExecuteReaderAsync());


            return dt.ToList<FederalReserveRouting>();
        }
    }
}