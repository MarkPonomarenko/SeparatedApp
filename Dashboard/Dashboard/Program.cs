using Shared.Repositories;
using Shared.Data;
using Shared.Data.Entities;
using Shared.Interfaces;
using Microsoft.EntityFrameworkCore;
using Shared.Utils;

namespace Dashboard
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<AppDbContext>(options => {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddScoped<IRepository<Order>, EntityRepository<Order>>();
            builder.Services.AddScoped<IRepository<Product>, EntityRepository<Product>>();
            builder.Services.AddScoped<IRepository<Category>, EntityRepository<Category>>();
            builder.Services.AddScoped<IUserRepositry, UserRepository>();
            builder.Services.AddScoped<IAccountManager, AccountManager>();
            builder.Services.AddCloudscribePagination();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}