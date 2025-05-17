using e_commerce_blackcat_api.Src.Models;
using e_commerce_blackcat_api.Src.Dtos;

namespace e_commerce_blackcat_api.Interfaces;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetAllUsersAsync();
    Task<IEnumerable<User>> SearchUser(string query);
    Task<User?> GetUserByIdAsync(string id);
    Task<User?> GetUserByEmail(string email);
    Task<bool> VerifyEmail(string email);
    Task<bool> VerifyUser(User user);
    Task<bool> CreateUserAsync(User user, ShippingAddres? shippingAddress);
    Task<bool> UpdateUserAsync(User user, EditUserDto editUserDto);
    Task<bool> UpdateShippingAddressAsync(UserDto userDto);
    Task<IList<string>> GetRoleAsync(User user);
    Task<(List<User>, int total)> GetPagedUserAsync(int page, int pageSize);
    Task<bool> DeleteUserAsync(User user, ShippingAddres? shippingAddress);
    Task<bool> ChangeUserState(User user, bool userStatus);
    Task<bool> ChangePassword(User user, string currentPassword, string newPassword);

}
