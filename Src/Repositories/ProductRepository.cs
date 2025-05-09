using e_commerce_blackcat_api.Data;
using e_commerce_blackcat_api.Interfaces;
using e_commerce_blackcat_api.Models;
using Microsoft.EntityFrameworkCore;

namespace e_commerce_blackcat_api.Repositories;

public class ProductRepository(DataContext context, ILogger<Product> logger) : IProductRepository
{
    private readonly DataContext _context = context;
    private readonly ILogger<Product> _logger = logger;

    public async Task AddAsync(Product product)
    {
        await _context.Products.AddAsync(product);
    }

    public void Delete(Product product)
    {
        _context.Products.Remove(product);
    }

    public async Task<Product?> GetByIdAsync(int id)
    {
        return await _context.Products.FindAsync(id)
            ?? throw new Exception("Product not found");
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await _context.Products.ToListAsync()
            ?? throw new Exception("No products found");
    }

    public IQueryable<Product> GetQueryableProducts()
    {
        return _context.Products.AsQueryable();
    }

    public async Task Update(Product product)
    {
        var existingProduct = await _context.Products.FindAsync(product.Id)
            ?? throw new Exception("Product not found");

        existingProduct.Title = product.Title;
        existingProduct.Description = product.Description;
        existingProduct.Price = product.Price;
        existingProduct.Stock = product.Stock;
        existingProduct.Category = product.Category;
        existingProduct.Brand = product.Brand;
        existingProduct.IsNew = product.IsNew;
        existingProduct.CreatedAt = product.CreatedAt;

        _context.Products.Update(existingProduct);
    }
}
