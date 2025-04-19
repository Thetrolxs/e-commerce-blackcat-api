using Bogus;
using e_commerce_blackcat_api.Models;
using e_commerce_blackcat_api.Src.Models;
using Microsoft.EntityFrameworkCore;

namespace e_commerce_blackcat_api.Data;

public static class DbInitializer
{
    public static void InitDb(WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<DataContext>()
            ?? throw new InvalidOperationException("Could not get DataContext");

        SeedData(context);
    }
    
    private static void SeedData(DataContext context)
    {
        // Aplica migraciones si faltan
        context.Database.Migrate();

        // Si ya hay productos, no hace nada
        if (context.Products.Any()) return;

        var faker = new Faker("es");

        var products = new Faker<Product>()
            .RuleFor(p => p.Title, f => f.Commerce.ProductName())
            .RuleFor(p => p.Description, f => f.Commerce.ProductDescription())
            .RuleFor(p => p.Price, f => decimal.Parse(f.Commerce.Price()))
            .RuleFor(p => p.Stock, f => f.Random.Int(10, 200))
            .RuleFor(p => p.Category, f => f.Commerce.Categories(1)[0])
            .RuleFor(p => p.Brand, f => f.Company.CompanyName())
            .RuleFor(p => p.IsNew, f => f.Random.Bool())
            .RuleFor(p => p.CreatedAt, f => f.Date.Past())
            .RuleFor(p => p.CartItems, f => new List<CartItem>())
            .RuleFor(p => p.OrderDetails, f => new List<OrderDetail>())
            .Generate(20);

        context.Products.AddRange(products);
        context.SaveChanges();
    }
}
