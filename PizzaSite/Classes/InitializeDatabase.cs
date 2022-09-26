using ContosoPizza.Data;
using ContosoPizza.Models;

namespace ContosoPizza.Classes
{
    public class InitializeDatabase
    {
        public static void Clean()
        {
            using var context = new ContosoPizzaContext(true);
            context.Database.EnsureCreated();
            context.Database.EnsureCreated();
        }

        public static void Populate()
        {
            List<Customer> customers = new List<Customer>();
            List<Order> orders = new List<Order>();


            using var context = new ContosoPizzaContext(true);


            context.SaveChanges();
        }
    }
}
