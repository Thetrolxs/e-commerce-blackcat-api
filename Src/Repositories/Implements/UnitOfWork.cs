using e_commerce_blackcat_api.Data;
using e_commerce_blackcat_api.Interfaces;
using e_commerce_blackcat_api.Src.Models;

using Microsoft.AspNetCore.Identity;

namespace e_commerce_blackcat_api.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly DataContext _context;

    public IProductRepository ProductRepository { get; }

    public IUserRepository Users { get; }

    public UserManager<User> UserManager { get; }
    public SignInManager<User> SignInManager { get; }

    public UnitOfWork(DataContext context, IProductRepository productRepository, IUserRepository userRepository, UserManager<User> userManager, SignInManager<User> signInManager)
    {
        _context = context;
        ProductRepository = productRepository;
        Users = userRepository;
        UserManager = userManager;
        SignInManager = signInManager;
    }

    public async Task<bool> CompleteAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }

    public void Dispose() => _context.Dispose();
}
