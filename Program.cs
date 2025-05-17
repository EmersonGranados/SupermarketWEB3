using Microsoft.EntityFrameworkCore;
using SupermarketWEB.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Threading.Tasks;
using SupermarketWEB.Models;


namespace SupermarketWEB
{
    public class Program
    {
        //PROYECTO LISTO Y FUNCIONAL 17/05/2025 12:02 A.M
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();

            //Agregando el contexto SupermarketContext a la aplicacion
            builder.Services.AddDbContext<SupermarketContext>(options =>
               options.UseSqlServer(builder.Configuration.GetConnectionString("SupermarketDB"))
               );

            builder.Services.AddAuthentication("MyCookieAuth")
               .AddCookie("MyCookieAuth", options =>
               {
                   options.Cookie.Name = "MyCookieAuth";
                   options.LoginPath = "/Account/Login";
                   options.AccessDeniedPath = "/Account/AccessDenied";
                   options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
               });

            builder.Services.AddAuthorization();

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<SupermarketContext>();

                  
                    context.Database.EnsureCreated();

                   
                    if (!context.Users.Any())
                    {
                        
                        context.Users.Add(new User
                        {
                            Email = "emer29granados@gmail.com",
                            Password = "admin123" 
                        });

                        await context.SaveChangesAsync();
                    }
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "Error al inicializar la base de datos");
                }
            }

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }
}
