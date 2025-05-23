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
        Task<IEnumerable<UserDto>> SearchUser(string query);
        Task<PageResultDto<UserPageDto>> GetFilteredUsersAsync(UserFilterDto filters);
        Task<UserDetailDto> GetUserDetailAsync(string userId);
        Task<PageResultDto<UserPageDto>> GetPagedUserAsync(int page);
        Task<UserDto> GetUser(ClaimsPrincipal claimsPrincipal);
        Task<bool> ChangeUserState(string id, bool userState, string reason);
        Task<bool> EditUser(ClaimsPrincipal userClaims, EditUserDto editUserDto);
        Task<bool> ChangeUserPassword(ClaimsPrincipal userClaims, ChangePasswordDto changePasswordDto);

    }
}