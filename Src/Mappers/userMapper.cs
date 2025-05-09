using e_commerce_blackcat_api.Src.Dtos;
using e_commerce_blackcat_api.Src.Models;

namespace e_commerce_blackcat_api.Src.Mappers;

public static class UserMapper
{
    public static UserDto MapToDto(User user) =>
        new()
        {
            Id = user.Id,
            FullName = user.FullName,
            Email = user.Email!,
            PhoneNumber = user.PhoneNumber!,
            Street = user.ShippingAddres?.Street,
            Number = user.ShippingAddres?.Number,
            Commune = user.ShippingAddres?.Commune,
            Region = user.ShippingAddres?.Region,
            PostalCode = user.ShippingAddres?.PostalCode
        };
}
