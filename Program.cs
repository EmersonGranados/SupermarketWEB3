using Microsoft.EntityFrameworkCore;
using SupermarketWEB.Data;
using Microsoft.AspNetCore.Authentication.Cookies;


namespace SupermarketWEB
{
    public class Program
    {
        public static void Main(string[] args)
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
