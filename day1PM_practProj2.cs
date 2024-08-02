// Practical Project 2: Building a .NET Application with Entity Framework Using GitHub Copilot (Console Application Version)
// Objective:
// By the end of this practical, you will be able to:
// - Build a fully functional .NET Console Application with Entity Framework Core.
// - Develop a multi-layered console application with a clear separation of concerns.
// - Use GitHub Copilot to assist in writing code, generating classes, and implementing features.
// - Write complex LINQ queries and integrate them into the application.

using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EFCoreConsoleApp.Models
{
    // Define the Product Model
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int StockQuantity { get; set; }
    }

    // Define the Data Context
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Product> Products { get; set; }
    }
}

class Program
{
    static void Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();
        using (var scope = host.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<AppDbContext>();

            // Sample usage:
            AddProduct(context, "Laptop", 1200m, "Gaming laptop", 10);
            ListProducts(context);
            UpdateProduct(context, 1, "Updated Laptop", 1150m, "Updated description", 8);
            DeleteProduct(context, 1);
            FindProduct(context, 2);
            ListLowStockProducts(context, 5);
        }
    }

    // Configure the Data Context and Dependency Injection
    static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureServices((_ services) =>
                services.AddDbContext<AppDbContext>(options =>
                    options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=EFCoreConsoleAppDb;Trusted_Connection=True;")));

    // Implement CRUD Operations
    static void AddProduct(AppDbContext context, string name, decimal price, string description, int stockQuantity)
    {
        var product = new Product { Name = name, Price = price, Description = description, StockQuantity = stockQuantity };
        context.Products.Add(product);
        context.SaveChanges();
    }

    static void ListProducts(AppDbContext context)
    {
        var products = context.Products.ToList();
        foreach (var product in products)
        {
            Console.WriteLine($"ID: {product.Id} Name: {product.Name} Price: {product.Price} Description: {product.Description} Stock: {product.StockQuantity}");
        }
    }

    static void UpdateProduct(AppDbContext context, int id, string name, decimal price, string description, int stockQuantity)
    {
        var product = context.Products.Find(id);
        if (product != null)
        {
            product.Name = name;
            product.Price = price;
            product.Description = description;
            product.StockQuantity = stockQuantity;
            context.SaveChanges();
        }
    }

    static void DeleteProduct(AppDbContext context, int id)
    {
        var product = context.Products.Find(id);
        if (product != null)
        {
            context.Products.Remove(product);
            context.SaveChanges();
        }
    }

    static void FindProduct(AppDbContext context, int id)
    {
        var product = context.Products.Find(id);
        if (product != null)
        {
            Console.WriteLine($"Found: ID: {product.Id} Name: {product.Name} Price: {product.Price} Description: {product.Description} Stock: {product.StockQuantity}");
        }
        else
        {
            Console.WriteLine("Product not found");
        }
    }

    // Implement LINQ Queries
    static void ListLowStockProducts(AppDbContext context, int stockThreshold)
    {
        var lowStockProducts = context.Products
            .Where(p => p.StockQuantity <= stockThreshold)
            .ToList();

        foreach (var product in lowStockProducts)
        {
            Console.WriteLine($"Low Stock - ID: {product.Id} Name: {product.Name} Stock: {product.StockQuantity}");
        }
    }
}
