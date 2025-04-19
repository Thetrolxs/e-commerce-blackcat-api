using Bogus;
using e_commerce_blackcat_api.Models;
using e_commerce_blackcat_api.Src.Models;

namespace e_commerce_blackcat_api.Data;

public static class ProductSeeder
{
    public static List<Product> GenerateFakeProducts(int quantity)
    {
        var faker = new Faker<Product>()
            .RuleFor(p => p.Title, f => f.Commerce.ProductName())
            .RuleFor(p => p.Description, f => f.Commerce.ProductDescription())
            .RuleFor(p => p.Price, f => decimal.Parse(f.Commerce.Price()))
            .RuleFor(p => p.Stock, f => f.Random.Int(0, 100))
            .RuleFor(p => p.Category, f => f.Commerce.Categories(1)[0])
            .RuleFor(p => p.Brand, f => f.Company.CompanyName())
            .RuleFor(p => p.IsNew, f => f.Random.Bool())
            .RuleFor(p => p.CreatedAt, f => f.Date.Past())
            .RuleFor(p => p.CartItems, f => new List<CartItem>())
            .RuleFor(p => p.OrderDetails, f => new List<OrderDetail>());

        return faker.Generate(quantity);
    }
}
