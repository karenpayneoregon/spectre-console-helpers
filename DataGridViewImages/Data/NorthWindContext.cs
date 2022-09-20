using ConfigurationLibrary.Classes;
using DataGridViewImages.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

#nullable disable

namespace DataGridViewImages.Data;

public partial class NorthWindContext : DbContext
{
    public NorthWindContext()
    {
    }

    public NorthWindContext(DbContextOptions<NorthWindContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Categories> Categories { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            StandardLogging(optionsBuilder);
        }
    }

    /// <summary>
    /// Setup connection
    /// Log to Visual Studio's output window
    /// </summary>
    public static void StandardLogging(DbContextOptionsBuilder optionsBuilder)
    {

        optionsBuilder
            .UseSqlServer(ConfigurationHelper.ConnectionString())
            .EnableSensitiveDataLogging()
            .LogTo(message => Debug.WriteLine(message));

    }
    /// <summary>
    /// Setup connection for no logging and no change tracking
    /// </summary>
    public static void NoLogging(DbContextOptionsBuilder optionsBuilder)
    {

        optionsBuilder
            .UseSqlServer(ConfigurationHelper.ConnectionString())
            .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new Configurations.CategoriesConfiguration());

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}