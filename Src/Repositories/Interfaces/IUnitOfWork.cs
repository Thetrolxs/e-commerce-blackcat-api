using System.Threading.Tasks;

using e_commerce_blackcat_api.Src.Models;

using Microsoft.AspNetCore.Identity;

namespace e_commerce_blackcat_api.Interfaces;

public interface IUnitOfWork : IDisposable
{
    // ... other repository properties or methods

    IProductRepository ProductRepository { get; }
    IUserRepository Users { get; }
    UserManager<User> UserManager { get; }
    SignInManager<User> SignInManager { get; }
    Task<bool> CompleteAsync();
}
