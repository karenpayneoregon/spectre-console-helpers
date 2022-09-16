using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OracleEntityFrameworkConsoleApp.Models;
using Microsoft.Extensions.Logging;
using ConfigurationLibrary.Classes;

namespace OracleEntityFrameworkConsoleApp.Data
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// The following is used as we are don't have a reason for change tracking.
    /// This is new to EF Core 6
    /// 
    /// DbContextOptionsBuilder.UseQueryTracking
    /// 
    /// Sets the tracking behavior for LINQ queries run against the context.
    /// Disabling change tracking is useful for read-only scenarios because it
    /// avoids the overhead of setting up change tracking for each entity instance. 
    /// https://docs.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.dbcontextoptionsbuilder.usequerytrackingbehavior?view=efcore-6.0
    /// </remarks>
    internal class OedContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            StandardLogging(optionsBuilder);
        }

        public virtual DbSet<LineFlagCodes> LineFlagCodes { get; set; }
        public virtual DbSet<FederalReserveRouting> FederalReserveRouting { get; set; }

        public void NoLogging(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder
                .UseOracle(ConfigurationHelper.ConnectionString())
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

        }
        public void StandardLogging(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder
                .UseOracle(ConfigurationHelper.ConnectionString())
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                .EnableSensitiveDataLogging()
                .LogTo(message => Debug.WriteLine(message), 
                    LogLevel.Information);

        }
    }
}
