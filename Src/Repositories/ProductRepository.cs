using e_commerce_blackcat_api.Data;
using e_commerce_blackcat_api.Interfaces;
using e_commerce_blackcat_api.Models;
using Microsoft.EntityFrameworkCore;
using e_commerce_blackcat_api.Helpers;

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

    public async Task<PagedResult<Product>> GetPagedProductsAsync(ProductParams queryParams)
{
    var query = _context.Products.AsQueryable();

    // Filtro: búsqueda por título o descripción
    if (!string.IsNullOrWhiteSpace(queryParams.Search))
    {
        query = query.Where(p =>
            p.Title.ToLower().Contains(queryParams.Search.ToLower()) ||
            p.Description.ToLower().Contains(queryParams.Search.ToLower()));
    }

    // Filtro: categoría
    if (!string.IsNullOrWhiteSpace(queryParams.Category))
    {
        query = query.Where(p => p.Category.ToLower() == queryParams.Category.ToLower());
    }

    // Filtro: marca
    if (!string.IsNullOrWhiteSpace(queryParams.Brand))
    {
        query = query.Where(p => p.Brand.ToLower() == queryParams.Brand.ToLower());
    }

    // Filtro: estado de novedad
    if (queryParams.IsNew.HasValue)
    {
        query = query.Where(p => p.IsNew == queryParams.IsNew.Value);
    }

    // Filtro: rango de precios
    if (queryParams.MinPrice.HasValue)
    {
        query = query.Where(p => p.Price >= queryParams.MinPrice.Value);
    }

    if (queryParams.MaxPrice.HasValue)
    {
        query = query.Where(p => p.Price <= queryParams.MaxPrice.Value);
    }

    // Ordenamiento
    query = queryParams.Sort?.ToLower() switch
    {
        "price" => query.OrderBy(p => p.Price),
        "price_desc" => query.OrderByDescending(p => p.Price),
        "title" => query.OrderBy(p => p.Title),
        "title_desc" => query.OrderByDescending(p => p.Title),
        _ => query.OrderBy(p => p.Id) // orden por defecto
    };

    // Total antes de paginar
    var totalCount = await query.CountAsync();

    // Paginación
    var products = await query
        .Skip((queryParams.PageNumber - 1) * queryParams.ValidatedPageSize)
        .Take(queryParams.ValidatedPageSize)
        .ToListAsync();

    return new PagedResult<Product>
    {
        Items = products,
        TotalCount = totalCount,
        PageNumber = queryParams.PageNumber,
        PageSize = queryParams.ValidatedPageSize
    };
}
}
