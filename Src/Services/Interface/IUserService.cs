using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

using e_commerce_blackcat_api.Src.Dtos;
using e_commerce_blackcat_api.Src.Models;

using Microsoft.AspNetCore.Identity;

namespace e_commerce_blackcat_api.Src.Services.Interface
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetUsers();
        Task<IEnumerable<UserDto>> SearchUsers(string query);
        Task<bool> EditUser(User user, EditUserDto editUserDto);
        Task<bool> DeleteUser(User user);
        Task<bool> ChangeUserPassword(ClaimsPrincipal userClaims, ChangePasswordDto changePasswordDto);

    }
}