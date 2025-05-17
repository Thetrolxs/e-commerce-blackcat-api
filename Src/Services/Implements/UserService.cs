using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

using e_commerce_blackcat_api.Interfaces;
using e_commerce_blackcat_api.Src.Dtos;
using e_commerce_blackcat_api.Src.Dtos.User;
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

            await _userRepository.ChangePassword(user, changePasswordDto.OldPassword, changePasswordDto.NewPassword);

            return true;
        }

        public async Task<bool> ChangeUserState(User user, bool userState)
        {
            if(user == null)
            {
                throw new Exception("El usuario no existe.");
            }

            var role = await _userRepository.GetRoleAsync(user);
            if(role == null)
            {
                throw new Exception("Error en el servidor.");
            }
            if(role.Contains("Administrador"))
            {
                throw new Exception("No se puede cambiar el estado del administrador.");
            }

            var result = await _userRepository.ChangeUserState(user, userState);
            return result;
        }

        public Task<bool> DeleteUser(User user)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> EditUser(ClaimsPrincipal userClaims, EditUserDto editUserDto)
        {
            var userId = userClaims.FindFirstValue(ClaimTypes.NameIdentifier);
            if(string.IsNullOrEmpty(userId))
            {
                return false;
            }

            var user = await _userRepository.GetUserByIdAsync(userId);
            if(user == null)
            {
                return false;
            }

            var result = await _userRepository.UpdateUserAsync(user, editUserDto);
            return result;
        }

        public async Task<PageResultDto<UserPageDto>> GetPagedUserAsync(int page)
        {
            const int pageSize = 20;

            var (users, total) = await _userRepository.GetPagedUserAsync(page, pageSize);

            var usersInPage = _mapperService.UserToUserPage(users);
            
            var result = new PageResultDto<UserPageDto>
            {
                CurrentPage = page,
                TotalCount = total,
                TotalPages = (int)Math.Ceiling(total / (double)pageSize),
                Items = usersInPage
            };

            return result;
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