using System.Threading.Tasks;

namespace e_commerce_blackcat_api.Interfaces;

public interface IUnitOfWork
{
    IProductRepository ProductRepository { get; }

    Task<int> CompleteAsync();
}
