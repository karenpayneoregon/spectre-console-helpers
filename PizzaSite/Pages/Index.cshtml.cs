using Microsoft.AspNetCore.Mvc.RazorPages;
using PizzaShop.Data;

namespace PizzaShop.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger, PizzaContext context)
        {
            _logger = logger;

            /*
             * Cheap way to warm-up EF Core for this demo
             * A better way is via a custom service or compiled models
             */
            _ = context.Customers.Count();
        }

        public void OnGet()
        {

        }
    }
}