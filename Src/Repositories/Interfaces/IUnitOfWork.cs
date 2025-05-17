using System.Threading.Tasks;

namespace e_commerce_blackcat_api.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IProductRepository Products { get; }
    IUserRepository Users { get; }

    Task<bool> CompleteAsync();
}
