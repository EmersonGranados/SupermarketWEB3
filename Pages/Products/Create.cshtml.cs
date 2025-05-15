using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SupermarketWEB.Data;
using SupermarketWEB.Models;

namespace SupermarketWEB.Pages.Products
{
    
    public class CreateModel : PageModel
    {
        private readonly SupermarketContext _context;
        public CreateModel(SupermarketContext context) => _context = context;

        [BindProperty]
        public Product Product { get; set; } = default!;

        public IActionResult OnGet()
        {
            ViewData["Categories"] = _context.Categories.ToList(); // Para dropdown
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.Products == null)
                return Page();

            _context.Products.Add(Product);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
    }
}
