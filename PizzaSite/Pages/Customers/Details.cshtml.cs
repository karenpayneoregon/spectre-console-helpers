﻿#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PizzaShop.Data;
using PizzaShop.Models;

namespace PizzaShop.Pages.Customers
{
    public class DetailsModel : PageModel
    {
        private readonly PizzaContext _context;

        public DetailsModel(PizzaContext context)
        {
            _context = context;
        }

        public Customer Customer { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Customer = await _context.Customers
                        .Include(c => c.Orders)
                        .ThenInclude(o => o.OrderDetails)
                        .ThenInclude(od => od.Product)
                        .FirstOrDefaultAsync(m => m.Id == id);


            if (Customer == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
