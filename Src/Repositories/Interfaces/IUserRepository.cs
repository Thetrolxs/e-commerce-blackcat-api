using e_commerce_blackcat_api.Src.Models;
using e_commerce_blackcat_api.Src.Dtos;
using e_commerce_blackcat_api.Src.Dtos.User;

namespace e_commerce_blackcat_api.Interfaces;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetAllUsersAsync();
    Task<IEnumerable<User>> SearchUser(string query);
    Task<(List<User>, int)> GetUsersWithFiltersAsync(UserFilterDto filters, int pageSize);
    Task<User?> GetUserByIdAsync(string id);
    Task<User?> GetUserByEmail(string email);
    Task<bool> VerifyEmail(string email);
    Task<bool> VerifyUser(User user);
    Task<bool> CreateUserAsync(User user);
    Task<bool> UpdateUserAsync(User user, EditUserDto editUserDto);
    Task<bool> UpdateShippingAddressAsync(UserDto userDto);
    Task<IList<string>> GetRoleAsync(User user);
    Task<(List<User>, int total)> GetPagedUserAsync(int page, int pageSize);
    Task<bool> DeleteUserAsync(User user, ShippingAddres? shippingAddress);
    Task<bool> ChangeUserState(User user, bool userStatus, string reason);
    Task<bool> ChangePassword(User user, string currentPassword, string newPassword);

}
