using e_commerce_blackcat_api.Data;
using e_commerce_blackcat_api.Interfaces;
using e_commerce_blackcat_api.Src.Models;
using e_commerce_blackcat_api.Src.Dtos;
using e_commerce_blackcat_api.Src.Mappers;
using Microsoft.EntityFrameworkCore;

namespace e_commerce_blackcat_api.Repositories;

public class UserRepository : IUserRepository
{
    private readonly DataContext _context;

    public UserRepository(DataContext context)
    {
        _context = context;
    }

    public async Task CreateUserAsync(User user, ShippingAddres? shippingAddress)
    {
        await _context.Users.AddAsync(user);
        if (shippingAddress != null)
        {
            await _context.ShippingAddres.AddAsync(shippingAddress);
        }
    }

    public void DeleteUserAsync(User user, ShippingAddres? shippingAddress)
    {
        if (shippingAddress != null)
            _context.ShippingAddres.Remove(shippingAddress);

        _context.Users.Remove(user);
    }

    public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
    {
        var users = await _context.Users.Include(u => u.ShippingAddres).ToListAsync();
        return users.Select(UserMapper.MapToDto);
    }

    public async Task<UserDto> GetUserByIdAsync(string id)
    {
        var user = await _context.Users
            .Include(u => u.ShippingAddres)
            .FirstOrDefaultAsync(u => u.Id == id)
            ?? throw new Exception("User not found");

        return UserMapper.MapToDto(user);
    }

    public void UpdateUserAsync(User user)
    {
        var existingUser = _context.Users.FirstOrDefault(x => x.Id == user.Id)
            ?? throw new Exception("User not found");

        existingUser.FullName = user.FullName;
        existingUser.Email = user.Email;
        existingUser.PhoneNumber = user.PhoneNumber;

        _context.Users.Update(existingUser);
    }

    public void UpdateShippingAddressAsync(UserDto userDto)
    {
        var user = _context.Users
            .Include(u => u.ShippingAddres)
            .FirstOrDefault(u => u.Id == userDto.Id)
            ?? throw new Exception("User not found");

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

        _context.Users.Update(user);
    }
}
