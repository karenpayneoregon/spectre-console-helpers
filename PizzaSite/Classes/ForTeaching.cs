using Microsoft.EntityFrameworkCore;
using PizzaShop.Data;
using PizzaShop.Models;

namespace PizzaShop.Classes;

public class ForTeaching
{
    public static async Task<Customer> GetCustomer(int id)
    {
        await using var context = new PizzaContext();
        return await context
            .Customers
            .Include(c => c.Orders)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public static async Task UpdateName(Customer currentCustomer)
    {
        await using var context = new PizzaContext();
        var customer = context.Customers.FirstOrDefault(c => c.Id == currentCustomer.Id);

        if (customer is not null)
        {
            customer.FirstName = currentCustomer.FirstName;
            customer.LastName = currentCustomer.LastName;
            context.Entry(customer).State = EntityState.Modified;
            context.Entry(customer).Property(p => p.Email).IsModified = false;
            await context.SaveChangesAsync();
        }
    }
}