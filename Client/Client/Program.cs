using Shared.Interfaces;
using Shared.Repositories;
using Shared.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using Shared.Data;
using Shared.Data.Entities;
using Shared.Utils.Mappings;

namespace Client
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
                options.UseSqlServer(connectionString);
            });
            builder.Services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<AppDbContext>();

            builder.Services.AddScoped<IRepository<Product>, EntityRepository<Product>>();
            builder.Services.AddScoped<IRepository<Order>, EntityRepository<Order>>();
            builder.Services.AddScoped<IRepository<Category>, EntityRepository<Category>>();
            builder.Services.AddScoped<IUserRepositry, UserRepository>();

            builder.Services.AddScoped<IAccountManager, AccountManager>();
            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddCloudscribePagination();
            builder.Services.AddAutoMapper(config =>
            {
                config.AddProfile(new UserMapping());
            }, typeof(Program).Assembly);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            app.UseAuthentication();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}