using e_commerce_blackcat_api.Data;
using e_commerce_blackcat_api.Interfaces;

namespace e_commerce_blackcat_api.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly DataContext _context;

    public IProductRepository ProductRepository { get; set; }
    public IUserRepository UserRepository { get; set; }

    public UnitOfWork(
        DataContext context,
        IProductRepository productRepository,
        IUserRepository userRepository)
    {
        _context = context;
        ProductRepository = productRepository;
        UserRepository = userRepository;
    }

    public async Task<int> CompleteAsync()
    {
        return await _context.SaveChangesAsync();
    }
}
