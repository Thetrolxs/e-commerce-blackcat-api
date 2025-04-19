using Microsoft.EntityFrameworkCore;
using e_commerce_blackcat_api.Data;
using e_commerce_blackcat_api.Interfaces;
using e_commerce_blackcat_api.Models;
using Microsoft.Extensions.Logging;

namespace e_commerce_blackcat_api.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly DataContext _context;
    private readonly ILogger<ProductRepository> _logger;

    public ProductRepository(DataContext context, ILogger<ProductRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<Product>> GetProductsAsync()
    {
        var products = await _context.Products.ToListAsync();

        if (products == null || !products.Any())
        {
            _logger.LogWarning("No products found in the database.");
            throw new Exception("No products found");
        }

        return products;
    }

    public async Task<Product> GetProductByIdAsync(int id)
    {
        var product = await _context.Products.FindAsync(id);

        if (product == null)
        {
            _logger.LogError("Product with ID {ProductId} not found", id);
            throw new Exception("Product not found");
        }

        return product;
    }

    public async Task AddProductAsync(Product product)
    {
        await _context.Products.AddAsync(product);
    }

    public void DeleteProductAsync(Product product)
    {
        _context.Products.Remove(product);
    }

    public async Task UpdateProductAsync(Product updatedProduct)
    {
        var existingProduct = await _context.Products.FindAsync(updatedProduct.Id);

        if (existingProduct == null)
        {
            _logger.LogError("Cannot update. Product with ID {ProductId} not found", updatedProduct.Id);
            throw new Exception("Product not found");
        }

        existingProduct.Title = updatedProduct.Title;
        existingProduct.Description = updatedProduct.Description;
        existingProduct.Price = updatedProduct.Price;
        existingProduct.Stock = updatedProduct.Stock;
        existingProduct.Category = updatedProduct.Category;
        existingProduct.Brand = updatedProduct.Brand;
        existingProduct.IsNew = updatedProduct.IsNew;
        existingProduct.CreatedAt = updatedProduct.CreatedAt;

        _context.Products.Update(existingProduct);
    }
}
