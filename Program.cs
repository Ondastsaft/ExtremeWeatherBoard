using ExtremeWeatherBoard.DAL;
using ExtremeWeatherBoard.Data;
using ExtremeWeatherBoard.Interfaces;
using ExtremeWeatherBoard.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Configuration;


namespace ExtremeWeatherBoard
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);
            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();
            builder.Services.AddScoped<UserManager<IdentityUser>>();
            builder.Services.AddScoped<DataRepository>();
            builder.Services.AddScoped<CategoryService>();
            builder.Services.AddScoped<SubCategoryService>();
            builder.Services.AddScoped<DiscussionThreadService>();
            builder.Services.AddScoped<CommentService>();
            builder.Services.AddScoped<UserDataService>();
            builder.Services.AddScoped<CategoryAPIService>();
            //builder.Services.AddScoped<MockDataGenerator>();
            builder.Services.AddRazorPages();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }
}
