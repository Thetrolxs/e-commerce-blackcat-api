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
        public DbSet<CartItem> CartItems => Set<CartItem>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<OrderDetail> OrderDetails => Set<OrderDetail>();

        protected override void OnModelCreating(ModelBuilder builder)
            {
                base.OnModelCreating(builder);

                //CartItem - User
                builder.Entity<CartItem>()
                    .HasOne(c => c.User)
                    .WithMany(u => u.CartItems)
                    .HasForeignKey(c => c.UserId);

                //CartItem - Product
                builder.Entity<CartItem>()
                    .HasOne(c => c.Product)
                    .WithMany(p => p.CartItems)
                    .HasForeignKey(c => c.ProductId);

                //Order - User
                builder.Entity<Order>()
                    .HasOne(o => o.User)
                    .WithMany(u => u.Orders)
                    .HasForeignKey(o => o.UserId);

                //OrderDetail - Order
                builder.Entity<OrderDetail>()
                    .HasOne(od => od.Order)
                    .WithMany(o => o.OrderDetails)
                    .HasForeignKey(od => od.OrderId);

                //OrderDetail - Product
                builder.Entity<OrderDetail>()
                    .HasOne(od => od.Product)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(od => od.ProductId);
            }        
    }
