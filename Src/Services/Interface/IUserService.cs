using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

using e_commerce_blackcat_api.Src.Dtos;
using e_commerce_blackcat_api.Src.Dtos.User;
using e_commerce_blackcat_api.Src.Models;

using Microsoft.AspNetCore.Identity;

namespace e_commerce_blackcat_api.Src.Services.Interface
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetUsers();
        Task<IEnumerable<UserDto>> SearchUsers(string query);
        Task<PageResultDto<UserPageDto>> GetPagedUserAsync(int page);
        Task<bool> ChangeUserState(User user, bool userState, string reason);
        Task<bool> EditUser(ClaimsPrincipal userClaims, EditUserDto editUserDto);
        Task<bool> DeleteUser(User user);
        Task<bool> ChangeUserPassword(ClaimsPrincipal userClaims, ChangePasswordDto changePasswordDto);

    }
}