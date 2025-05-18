using e_commerce_blackcat_api.Models;
using e_commerce_blackcat_api.Helpers;

namespace e_commerce_blackcat_api.Interfaces;
public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllAsync();
    Task<PagedResult<Product>> GetPagedProductsAsync(ProductParams queryParams);
    Task<Product?> GetByIdAsync(int id);
    Task AddAsync(Product product);
    Task Update(Product product);
    void Delete(Product product);
}
