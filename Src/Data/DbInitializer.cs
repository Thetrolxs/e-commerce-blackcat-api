using Bogus;
using e_commerce_blackcat_api.Models;
using e_commerce_blackcat_api.Src.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Bogus.DataSets;

namespace e_commerce_blackcat_api.Data;

public static class DbInitializer
{
    public static void InitDb(WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<DataContext>()
            ?? throw new InvalidOperationException("Could not get DataContext");

        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();      
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>(); 

        SeedData(context, userManager, roleManager);
    }
    
    private static void SeedData(DataContext context, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
    {
        // Aplica migraciones si faltan
        context.Database.Migrate();

        // Si no hay productos, insertarlos
        if (!context.Products.Any())
        {
            var products = new Faker<Product>("es")
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
                .RuleFor(p => p.Image, f => f.Image.PicsumUrl())
                .Generate(20);

            context.Products.AddRange(products);
            context.SaveChanges();
        }

        // Crear roles si no existen
        var roles = new[] { "Administrador", "Cliente" };
        foreach (var role in roles)
        {
            if (!roleManager.RoleExistsAsync(role).Result)
            {
                roleManager.CreateAsync(new IdentityRole(role)).Wait();
            }
        }

        // Crear usuario admin si no existe
        const string adminEmail = "ignacio.mancilla@gmail.com";
        const string adminPassword = "Pa$$word2025";
        var adminUser = userManager.FindByEmailAsync(adminEmail).Result;

        if (adminUser == null)
        {
            adminUser = new User
            {
                UserName = adminEmail,
                Email = adminEmail,
                FullName = "Ignacio Mancilla",
                PhoneNumber = "1234567890",
                Birthday = new DateTime(2000, 10, 25),
                UserRegister = DateTime.UtcNow,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                IsActive = true,
            };
            var result = userManager.CreateAsync(adminUser, adminPassword).Result;
            if (result.Succeeded)
            {
                userManager.AddToRoleAsync(adminUser, "Administrador").Wait();
            }
        }

        // Si hay menos de 2 usuarios (admin incluido), crea usuarios cliente
        if (userManager.Users.Count() < 2)
        {
            var users = new Faker<User>("es")
                .RuleFor(u => u.FullName, f => f.Name.FullName())
                .RuleFor(u => u.Email, (f, u) => f.Internet.Email(u.FullName))
                .RuleFor(u => u.UserName, (f, u) => u.Email)
                .RuleFor(u => u.Birthday, f => f.Date.Past(40, DateTime.UtcNow.AddYears(-18))) // Edad entre 18-58
                .RuleFor(u => u.UserRegister, f => f.Date.Past(2))
                .RuleFor(u => u.LastAccess, f => f.Date.Recent(30))
                .RuleFor(u => u.IsActive, f => f.Random.Bool(0.85f)) // 85% activos
                .RuleFor(u => u.DesactivationReason, (f, u) => u.IsActive ? null : f.Lorem.Sentence())
                .RuleFor(u => u.EmailConfirmed, f => true)
                .RuleFor(u => u.PhoneNumber, f => f.Phone.PhoneNumber("9########"))
                .RuleFor(u => u.PhoneNumberConfirmed, f => true)
                .Generate(40);

            foreach (var user in users)
            {
                var result = userManager.CreateAsync(user, "Pa$$word2025").Result;
                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Cliente").Wait();
                }
            }
        }
    }
}
