using e_commerce_blackcat_api.Src.Models;
using e_commerce_blackcat_api.Src.Dtos;
using e_commerce_blackcat_api.Interfaces;

namespace e_commerce_blackcat_api.Interfaces;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetAllUsersAsync();
    Task<IEnumerable<User>> SearchUser(string query);
    Task<UserDto> GetUserByIdAsync(string id);
    Task<bool> VerifyEmail(string email);
    Task<bool> VerifyUser(User user);
    Task<bool> CreateUserAsync(User user, ShippingAddres? shippingAddress);
    Task<bool> UpdateUserAsync(User user, EditUserDto editUserDto);
    Task<bool> UpdateShippingAddressAsync(UserDto userDto);
    Task<IList<string>> GetRoleAsync(User user);
    Task<bool> DeleteUserAsync(User user, ShippingAddres? shippingAddress);
    Task<bool> ChangePassword(User user, string currentPassword, string newPassword);

}
