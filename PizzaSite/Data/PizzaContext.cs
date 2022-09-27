using Microsoft.EntityFrameworkCore;
using ContosoPizza.Models;
using ConfigurationLibrary.Classes;

namespace ContosoPizza.Data
{
    public partial class PizzaContext : DbContext
    {
        private readonly bool _create;

        public PizzaContext()
        {
        }

        public PizzaContext(bool create)
        {
            _create = create;
        }

        public PizzaContext(DbContextOptions<PizzaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Customer> Customers { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<OrderDetail> OrderDetails { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!_create) return;
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(ConfigurationHelper.ConnectionString());
            }

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
