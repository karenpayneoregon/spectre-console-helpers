using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using ContosoPizza.Models;
using ConfigurationLibrary.Classes;
using ContosoPizza.Pages.Customers;

namespace ContosoPizza.Data
{
    public partial class ContosoPizzaContext : DbContext
    {
        private readonly bool _create;

        public ContosoPizzaContext()
        {
        }

        public ContosoPizzaContext(bool create)
        {
            _create = create;
        }

        public ContosoPizzaContext(DbContextOptions<ContosoPizzaContext> options)
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
