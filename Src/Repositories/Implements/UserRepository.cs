using e_commerce_blackcat_api.Data;
using e_commerce_blackcat_api.Interfaces;
using e_commerce_blackcat_api.Src.Models;
using e_commerce_blackcat_api.Src.Dtos;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using e_commerce_blackcat_api.Src.Dtos.User;

namespace e_commerce_blackcat_api.Repositories;

public class UserRepository : IUserRepository
{
    private readonly DataContext _context;
    private readonly UserManager<User> _userManager;

    public UserRepository(DataContext context, UserManager<User> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task<bool> CreateUserAsync(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteUserAsync(User user, ShippingAddres? shippingAddress)
    {
        var existingUser = _context.Users.FirstOrDefault(x => x.Id == user.Id);
        if(existingUser == null){
            return false;
        }     
        if (shippingAddress != null){
            _context.ShippingAddres.Remove(shippingAddress);
        }
        _context.Entry(existingUser).State = EntityState.Deleted;
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task<User?> GetUserByIdAsync(string id)
    {
        var user = await _context.Users
            .Include(u => u.ShippingAddres)
            .FirstOrDefaultAsync(u => u.Id == id)
            ?? throw new Exception("User not found");

        return user;
    }

    public async Task<bool> UpdateUserAsync(User user, EditUserDto editUserDto)
    {
        var existingUser = _context.Users.FirstOrDefault(x => x.Id == user.Id)
            ?? throw new Exception("User not found");
        if(existingUser == null){
            return false;
        }

        existingUser.FullName = editUserDto.FullName ?? existingUser.FullName;
        existingUser.Email = editUserDto.Email ?? existingUser.Email;
        existingUser.PhoneNumber = editUserDto.PhoneNumber ?? existingUser.PhoneNumber;
        existingUser.Birthday = editUserDto.Birthday ?? existingUser.Birthday;

        _context.Entry(existingUser).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> UpdateShippingAddressAsync(UserDto userDto)
    {
        var user = _context.Users
            .Include(u => u.ShippingAddres)
            .FirstOrDefault(u => u.Id == userDto.Id)
            ?? throw new Exception("User not found");
        
        if(userDto == null)
        {
            return false;
        }

        if (user.ShippingAddres == null)
        {
            user.ShippingAddres = new ShippingAddres
            {
                Street = userDto.Street!,
                Number = userDto.Number!,
                Commune = userDto.Commune!,
                Region = userDto.Region!,
                PostalCode = userDto.PostalCode!,
                UserId = user.Id
            };
        }
        else
        {
            user.ShippingAddres.Street = userDto.Street!;
            user.ShippingAddres.Number = userDto.Number!;
            user.ShippingAddres.Commune = userDto.Commune!;
            user.ShippingAddres.Region = userDto.Region!;
            user.ShippingAddres.PostalCode = userDto.PostalCode!;
        }

        _context.Entry(user).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<IEnumerable<User>> SearchUser(string query)
    {
        var users = await _context.Users.Where(u => u.Id.ToString().Contains(query)
                                        || u.FullName.Contains(query)
                                        || (u.Email != null && u.Email.Contains(query)))
                                        .ToListAsync();

        return users;
    }
    
    public async Task<(List<User>, int)> GetUsersWithFiltersAsync(UserFilterDto filters, int pageSize)
    {
        var query = _context.Users.AsQueryable();

        if (filters.IsActive.HasValue)
            query = query.Where(u => u.IsActive == filters.IsActive);

        if (!string.IsNullOrEmpty(filters.Email))
            query = query.Where(u => u.Email!.Contains(filters.Email));

        if (!string.IsNullOrEmpty(filters.NameOrLastName))
            query = query.Where(u => u.FullName.Contains(filters.NameOrLastName));

        if (filters.FromRegisterDate.HasValue)
            query = query.Where(u => u.UserRegister >= filters.FromRegisterDate);

        if (filters.ToRegisterDate.HasValue)
            query = query.Where(u => u.UserRegister <= filters.ToRegisterDate);

        var total = await query.CountAsync();

        var users = await query
            .OrderByDescending(u => u.UserRegister)
            .Skip((filters.Page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (users, total);
    }

    public async Task<bool> VerifyEmail(string email)
    {
        var user = await _context.Users.Where(u => u.Email == email).FirstOrDefaultAsync();
        if (user == null)
        {
            return false;
        }

        return true;
    }

    public Task<bool> VerifyUser(User user)
    {
        var existingUser = _context.Users.FirstOrDefaultAsync(x => x.Id == user.Id);
        if(existingUser == null){
            return Task.FromResult(false);
        }

        return Task.FromResult(true);
    }

    public async Task<bool> ChangePassword(User user, string currentPassword, string newPassword)
    {
        var existingUser = _context.Users.FirstOrDefaultAsync(x => x.Id == user.Id);
        if(existingUser == null){
            return false;
        }

        await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);

        return true;
    }

    public async Task<bool> ChangeUserState(User user, bool userStatus, string reason)
    {
        if(user == null)
        {
            return false;
        }

        if (string.IsNullOrEmpty(reason))
        {
            reason = "Contactar con el administrador.";
        }

        user.IsActive = userStatus;
        _context.Entry(user).State = EntityState.Modified;
        user.DesactivationReason = reason;
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<User?> GetUserByEmail(string email)
    {
        var user = await _context.Users.Where(u => u.Email == email)
                                            .FirstOrDefaultAsync();

        return user;
    }

    public async Task<IList<string>> GetRoleAsync(User user)
    {
        return await _userManager.GetRolesAsync(user);
    }

    public async Task<(List<User>, int total)> GetPagedUserAsync(int page, int pageSize)
    {
        var query = _userManager.Users.AsQueryable();

        var total = await query.CountAsync();

        var users = await query
                            .Skip((page - 1) * pageSize)
                            .Take(pageSize)
                            .ToListAsync();

        return (users, total);
    }
}
