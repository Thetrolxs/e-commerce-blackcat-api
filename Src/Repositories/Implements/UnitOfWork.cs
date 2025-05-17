using e_commerce_blackcat_api.Data;
using e_commerce_blackcat_api.Interfaces;
using e_commerce_blackcat_api.Src.Models;

namespace e_commerce_blackcat_api.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly DataContext _context;

    public IProductRepository Products { get; }

    public IUserRepository Users { get; }

    public UnitOfWork(DataContext context, IProductRepository productRepository, IUserRepository userRepository)
    {
        _context = context;
        Products = productRepository;
        Users = userRepository;
    }

    public async Task<bool> CompleteAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }

    public void Dispose() => _context.Dispose();
}
