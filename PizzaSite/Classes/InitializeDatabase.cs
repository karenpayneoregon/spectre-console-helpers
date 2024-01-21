using PizzaShop.Data;
using PizzaShop.Models;

namespace PizzaShop.Classes;

public class InitializeDatabase
{
    /// <summary>
    /// First, if the database exists, delete it
    /// Second, create the database based on the models
    /// </summary>
    public static void Clean()
    {
        using var context = new PizzaContext(true);
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
    }

    /// <summary>
    /// Populate tables
    /// * SaveChanges will return how many rows were affected
    ///   * For each new record, after saving they will have a primary key set
    /// * Customers and Products must be populated first so we have
    ///   keys to use with orders.
    /// </summary>
    public static void Populate()
    {
        List<Customer> customers = new List<Customer>
        {
            new ()
            {
                FirstName = "Jane", 
                LastName = "Smith", 
                Address = "123 Maple Ave", 
                Phone = "444-555-5555", 
                Email = "smith@star.com"
            },
            new ()
            {
                FirstName = "Mike", 
                LastName = "Jones", 
                Address = "967 Ne South Street",
                Phone = "354-535-5353", 
                Email = "jones@yahoo.com"
            }
        };

        List<Product> products = new()
        {
            new () { Name = "Veggie Pizza (small)", Price = 5 },
            new () { Name = "Veggie Pizza (medium)", Price = 8 },
            new () { Name = "Veggie Pizza (large)", Price = 10},
            new () { Name = "Pepperoni Pizza (small)", Price = 7 },
            new () { Name = "Pepperoni Pizza (medium)", Price = 9 },
            new () { Name = "Pepperoni Pizza (large)", Price = 10 },
            new () { Name = "Margherita Pizza (small)", Price = 8 },
            new () { Name = "Margherita Pizza (medium)", Price = 10 },
            new () { Name = "Margherita Pizza (large)", Price = 12 },
            new () { Name = "BBQ Chicken Pizza (small)", Price = 12 },
            new () { Name = "BBQ Chicken Pizza (medium)", Price = 14 },
            new () { Name = "BBQ Chicken Pizza (large)", Price = 16 }
        };

        /*
         * Here we use an overloaded constructor suited for
         * initial population of database records
         */
        using var context = new PizzaContext(true);
        context.AddRange(customers);
        context.AddRange(products);

        context.SaveChanges();

        List<Order> orders = new()
        {
            new ()
            {
                CustomerId = 1, 
                OrderPlaced = new DateTime(2022,9,24,13,30,0), 
                OrderFulfilled = new DateTime(2022, 9, 24, 14, 15, 0), 
                OrderDetails = new List<OrderDetail>() {new ()
                {
                    ProductId = 2, Quantity = 2, OrderId = 1
                }}
            },
            new ()
            {
                CustomerId = 2,
                OrderPlaced = new DateTime(2022, 9, 24, 18, 30, 0),
                OrderFulfilled = new DateTime(2022, 9, 22, 18, 22, 0),
                OrderDetails = new List<OrderDetail>()
                {
                    new() { ProductId = 4, Quantity = 1, OrderId = 2},
                    new() { ProductId = 6, Quantity = 1, OrderId = 2},
                }
            }
        };

        context.AddRange(orders);

        context.SaveChanges();

    }
}