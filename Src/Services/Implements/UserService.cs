using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

using e_commerce_blackcat_api.Interfaces;
using e_commerce_blackcat_api.Src.Dtos;
using e_commerce_blackcat_api.Src.Models;
using e_commerce_blackcat_api.Src.Services.Interface;

using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;

namespace e_commerce_blackcat_api.Src.Services.Implements
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapperService _mapperService;

        public UserService(IUserRepository userRepository, IMapperService mapperService){
            _userRepository = userRepository;
            _mapperService = mapperService;
        }

        public async Task<bool> ChangeUserPassword(ClaimsPrincipal userClaims, ChangePasswordDto changePasswordDto)
        {
            var userId = userClaims.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                return false;
            }

            var user = await _userRepository.GetUserByIdAsync(userId);
            if(user == null)
            {
                return false;
            }

            await _userRepository.ChangePassword(_mapperService.UserDtoToUser(user), changePasswordDto.OldPassword, changePasswordDto.NewPassword);

            return true;
        }

        public Task<bool> DeleteUser(User user)
        {
            throw new NotImplementedException();
        }

        public Task<bool> EditUser(User user, EditUserDto editUserDto)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<UserDto>> GetUsers()
        {
            var users = await _userRepository.GetAllUsersAsync();
            var mappedUser = _mapperService.MapUsers(users);
            return mappedUser;
        }

        public async Task<IEnumerable<UserDto>> SearchUsers(string query)
        {
            var users = await _userRepository.SearchUser(query);
            var mappedUser = _mapperService.MapUsers(users);
            return mappedUser;
        }
    }
}