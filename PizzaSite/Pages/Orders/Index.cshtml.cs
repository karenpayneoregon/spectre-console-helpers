using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PizzaShop.Data;
using PizzaShop.Models;

namespace PizzaShop.Pages.Orders
{
    public class IndexModel : PageModel
    {
        private readonly PizzaContext _context;

        public IndexModel(PizzaContext context)
        {
            _context = context;
        }

        public IList<Order> Order { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Orders is not null)
            {
                Order = await _context
                    .Orders.Include(o => o.Customer)
                    .OrderBy(x => x.OrderPlaced)
                    .ToListAsync();
            }
        }
    }
}
