using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SupermarketWEB.Data;
using SupermarketWEB.Models;

namespace SupermarketWEB.Pages.Account
{
    public class RegisterModel : PageModel
    {
        
        
            private readonly SupermarketContext _context;

            public RegisterModel(SupermarketContext context)
            {
                _context = context;
            }

            [BindProperty]
            public Models.RegisterModel RegisterData { get; set; } 

            public string SuccessMessage { get; set; }
            public string ErrorMessage { get; set; }

            public void OnGet()
            {
            }

            public async Task<IActionResult> OnPostAsync()
            {
                if (!ModelState.IsValid)
                {
                    return Page();
                }

               
                var existingUser = await _context.Users
                    .FirstOrDefaultAsync(u => u.Email == RegisterData.Email); 

                if (existingUser != null)
                {
                    ErrorMessage = "Este email ya está registrado";
                    return Page();
                }

             
                var newUser = new User
                {
                    Email = RegisterData.Email, 
                    Password = RegisterData.Password 
                };

                _context.Users.Add(newUser);
                await _context.SaveChangesAsync();

                SuccessMessage = "Registro exitoso. Ahora puedes iniciar sesión.";
                return RedirectToPage("/Account/Login", new { successMessage = SuccessMessage });
            }
        }
    }

