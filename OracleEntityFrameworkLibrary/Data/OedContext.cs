using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OracleEntityFrameworkLibrary.Models;
using static ConfigurationLibrary.Classes.ConfigurationHelper;

namespace OracleEntityFrameworkLibrary.Data
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
    public class OedContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // log information to Visual Studio's Output window
            StandardLogging(optionsBuilder);
        }

        public virtual DbSet<LineFlagCodes> LineFlagCodes { get; set; }
        public virtual DbSet<FederalReserveRouting> FederalReserveRouting { get; set; }

        /// <summary>
        /// Setup
        /// * Connection string
        /// * Indicate not to track changes for all read statements
        ///
        /// This is for production
        /// </summary>
        public void NoLogging(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder
                .UseOracle(ConnectionString())
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

        }

        /// <summary>
        /// Setup
        /// * Connection string
        /// * Indicate not to track changes for all read statements
        /// * Enable specific logging to the Output window
        ///
        /// This is for non-prod environments
        /// </summary>
        public void StandardLogging(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder
                .UseOracle(ConnectionString())
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                .EnableSensitiveDataLogging()
                .LogTo(message => Debug.WriteLine(message), 
                    LogLevel.Information);

        }
    }
}
