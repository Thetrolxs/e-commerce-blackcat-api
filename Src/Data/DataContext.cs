using Microsoft.EntityFrameworkCore;
using e_commerce_blackcat_api.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using e_commerce_blackcat_api.Src.Models;

namespace e_commerce_blackcat_api.Data;
    public class DataContext : IdentityDbContext<User>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<ShippingAddres> ShippingAddres { get; set; } = null!;
        public DbSet<Product> Products => Set<Product>();
        public new DbSet<User> Users => Set<User>();
    }
