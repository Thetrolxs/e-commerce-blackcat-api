using Microsoft.EntityFrameworkCore;
using e_commerce_blackcat_api.Models;

namespace e_commerce_blackcat_api.Data;
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Product> Products => Set<Product>();

    }
