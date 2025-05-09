using e_commerce_blackcat_api.Models;
namespace e_commerce_blackcat_api.Interfaces;
public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllAsync();
    Task<Product?> GetByIdAsync(int id);
    Task AddAsync(Product product);
    Task Update(Product product);
    void Delete(Product product);
}
