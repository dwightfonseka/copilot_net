// Lab Exercise 8: .NET with Entity Framework for Copilot
// Objective:
// By the end of this lab exercise, you will be able to:
// - Create a .NET Console Application using Entity Framework in Microsoft Visual Studio Community 2022.
// - Define data models and set up a data context.
// - Configure dependency injection for the data context.
// - Develop a console application that performs CRUD operations on the database.
// - Utilize GitHub Copilot to assist in writing code, generating methods, and handling changes.

using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EFCoreConsoleApp.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }

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

            // Database logic here
            AddProduct(context, "Laptop", 1200m);
            ListProducts(context);
            UpdateProduct(context, 1, "Updated Laptop", 1150m);
            DeleteProduct(context, 1);
            FindProduct(context, 2);
        }
    }

    static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureServices((_ services) =>
                services.AddDbContext<AppDbContext>(options =>
                    options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=EFCoreConsoleAppDb;Trusted_Connection=True;")));

    static void AddProduct(AppDbContext context, string name, decimal price)
    {
        var product = new Product { Name = name, Price = price };
        context.Products.Add(product);
        context.SaveChanges();
    }

    static void ListProducts(AppDbContext context)
    {
        var products = context.Products.ToList();
        foreach (var product in products)
        {
            Console.WriteLine($"ID: {product.Id} Name: {product.Name} Price: {product.Price}");
        }
    }

    static void UpdateProduct(AppDbContext context, int id, string name, decimal price)
    {
        var product = context.Products.Find(id);
        if (product != null)
        {
            product.Name = name;
            product.Price = price;
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
            Console.WriteLine($"Found: ID: {product.Id} Name: {product.Name} Price: {product.Price}");
        }
        else
        {
            Console.WriteLine("Product not found");
        }
    }
}

// Lab Exercise 9: .NET with Entity Framework for Copilot (Console Application Version)
// Objective:
// By the end of this lab exercise, you will be able to:
// - Create a full-featured .NET Console Application backed by Entity Framework Core.
// - Develop a console application that interacts with a database using Entity Framework Core.
// - Utilize GitHub Copilot to assist in generating code that aligns with your development intentions, minimizing the need for manual corrections.

namespace EFCoreConsoleApp.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
    }
}

// Reuse the previous AppDbContext class for database context

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
            AddProduct(context, "Laptop", 1200m, "Gaming laptop");
            ListProducts(context);
            UpdateProduct(context, 1, "Updated Laptop", 1150m, "Updated description");
            DeleteProduct(context, 1);
            FindProduct(context, 2);
        }
    }

    static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureServices((_ services) =>
                services.AddDbContext<AppDbContext>(options =>
                    options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=EFCoreConsoleAppDb;Trusted_Connection=True;")));

    static void AddProduct(AppDbContext context, string name, decimal price, string description)
    {
        var product = new Product { Name = name, Price = price, Description = description };
        context.Products.Add(product);
        context.SaveChanges();
    }

    static void ListProducts(AppDbContext context)
    {
        var products = context.Products.ToList();
        foreach (var product in products)
        {
            Console.WriteLine($"ID: {product.Id} Name: {product.Name} Price: {product.Price} Description: {product.Description}");
        }
    }

    static void UpdateProduct(AppDbContext context, int id, string name, decimal price, string description)
    {
        var product = context.Products.Find(id);
        if (product != null)
        {
            product.Name = name;
            product.Price = price;
            product.Description = description;
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
            Console.WriteLine($"Found: ID: {product.Id} Name: {product.Name} Price: {product.Price} Description: {product.Description}");
        }
        else
        {
            Console.WriteLine("Product not found");
        }
    }
}

// Lab Exercise 10: Building a .NET Application with Entity Framework Using GitHub Copilot (Console Application Version)
// Objective:
// By the end of this lab exercise, you will be able to:
// - Build a fully functional .NET Console Application with Entity Framework Core.
// - Develop a multi-layered console application with a clear separation of concerns.
// - Use GitHub Copilot to assist in writing code, generating classes, and implementing features.
// - Write complex LINQ queries and integrate them into the application.

namespace EFCoreConsoleApp.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int StockQuantity { get; set; }
    }
}

// Reuse the previous AppDbContext class for database context

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

    static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureServices((_ services) =>
                services.AddDbContext<AppDbContext>(options =>
                    options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=EFCoreConsoleAppDb;Trusted_Connection=True;")));

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
        var product = context.Products.Find