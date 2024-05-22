using la_mia_pizzeria_crud_mvc.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;

namespace la_mia_pizzeria_crud_mvc
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // ELIMINO QUESTA RIGA -> var connectionString = builder.Configuration.GetConnectionString("PizzaContextConnection") ?? throw new InvalidOperationException("Connection string 'PizzaContextConnection' not found.");
           
            //MODIFICA MIA
            builder.Services.AddDbContext<PizzaContext>();

            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<PizzaContext>();

            // Add services to the container.
            builder.Services.AddControllersWithViews();

           
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            //MODIFICA MIA
            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Pizza}/{action=Index}/{id?}");

            //MODIFICA MIA
            app.MapRazorPages();

            app.Run();
        }

   
    }
}