using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OracleEntityFrameworkConsoleApp.Models;
using Microsoft.Extensions.Logging;

namespace OracleEntityFrameworkConsoleApp.Data
{
    internal class OedContext : DbContext
    {
        /// <summary>
        /// Cheap way to ensure sensitive information is not shown and/or placed
        /// into the Git repo.
        /// </summary>
        private string ConnectionString() 
            => File.ReadAllText(@"C:\OED\DotnetLand\VS2022\OracleConnections\DevOcs.txt");

        public OedContext()
        {
        }

        public OedContext(DbContextOptions<OedContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            StandardLogging(optionsBuilder);
        }

        public virtual DbSet<LineFlagCodes> LineFlagCodes { get; set; }

        public void NoLogging(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder
                .UseOracle(ConnectionString())
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

        }
        public void StandardLogging(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder
                .UseOracle(ConnectionString())
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                .EnableSensitiveDataLogging()
                .LogTo(message => Debug.WriteLine(message), LogLevel.Information);

        }
    }
}
