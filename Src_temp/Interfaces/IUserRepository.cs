using e_commerce_blackcat_api.Src.Models;
using e_commerce_blackcat_api.Src.Dtos;
using e_commerce_blackcat_api.Interfaces;

namespace e_commerce_blackcat_api.Interfaces;

public interface IUserRepository
{
    Task<IEnumerable<UserDto>> GetAllUsersAsync();
    Task<UserDto> GetUserByIdAsync(string id);
    Task CreateUserAsync(User user, ShippingAddres? shippingAddress);
    void UpdateUserAsync(User user);
    void UpdateShippingAddressAsync(UserDto userDto);
    void DeleteUserAsync(User user, ShippingAddres? shippingAddress);
}
