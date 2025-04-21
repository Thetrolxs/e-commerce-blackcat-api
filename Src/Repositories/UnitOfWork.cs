using e_commerce_blackcat_api.Data;
using e_commerce_blackcat_api.Interfaces;

namespace e_commerce_blackcat_api.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly DataContext _context;

    public IProductRepository Products { get; }

    public UnitOfWork(DataContext context, IProductRepository productRepository)
    {
        _context = context;
        Products = productRepository;
    }

    public async Task<int> CompleteAsync()
    {
        return await _context.SaveChangesAsync();
    }
}
