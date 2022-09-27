using ConfigurationLibrary.Classes;
using Microsoft.EntityFrameworkCore;
using PizzaShop.Classes;
using PizzaShop.Data;

namespace PizzaShop;

class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddRazorPages();
        builder.Services.AddDbContext<PizzaContext>(options =>
            options.UseSqlServer(ConfigurationHelper.ConnectionString()));

        WebApplication app = builder.Build();

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
        app.UseAuthorization();
        app.MapRazorPages();

        if (Environment.GetEnvironmentVariable("CREATE_DB") == "Yes")
        {
            InitializeDatabase.Clean();
            InitializeDatabase.Populate();
        }

        app.Run();
    }
}