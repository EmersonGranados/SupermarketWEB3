using SupermarketWEB.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SupermarketWEB.Data;
using Microsoft.AspNetCore.Authorization;

namespace SupermarketWEB.Pages.Products
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly SupermarketContext _context;

        public IndexModel(SupermarketContext context)
        {
            _context = context;
        }

        public IList<Product> Products { get; set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Products != null)
            {
                Products = await _context.Products
                    .Include(p => p.Category) // Carga relacionada
                    .ToListAsync();
            }
        }
    }
}