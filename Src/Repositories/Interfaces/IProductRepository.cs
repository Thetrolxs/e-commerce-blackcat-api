using e_commerce_blackcat_api.Models;
namespace e_commerce_blackcat_api.Interfaces;
public interface IProductRepository
{
    Task<IEnumerable<Product>> GetProductsAsync();
    Task<Product> GetProductByIdAsync(int id);
    Task AddProductAsync(Product product);
    Task UpdateProductAsync(Product product);
    void DeleteProductAsync(Product product);
}
