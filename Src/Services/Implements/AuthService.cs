using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

using e_commerce_blackcat_api.Interfaces;
using e_commerce_blackcat_api.Src.Dtos.User;
using e_commerce_blackcat_api.Src.Models;
using e_commerce_blackcat_api.Src.Services.Interface;

using Microsoft.IdentityModel.Tokens;

namespace e_commerce_blackcat_api.Src.Services.Implements
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapperService _mapperService;
        private readonly IConfiguration _configuration;

        public AuthService(IUnitOfWork unitOfWork, IConfiguration configuration, IMapperService mapperService)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
            _mapperService = mapperService;
        }
        public async Task<LoggedUserDto> Login(LoginUserDto loginUserDto)
        {
            var user = await _unitOfWork.Users.GetUserByEmail(loginUserDto.Email.ToString());
            if (user == null)
            {
                throw new UnauthorizedAccessException("Credenciales incorrectas, intente nuevamente");
            }
            if (!user.IsActive)
            {
                throw new UnauthorizedAccessException(user.DesactivationReason);
            }

            var result = await _unitOfWork.SignInManager.CheckPasswordSignInAsync(user, loginUserDto.Password, false);

            if (!result.Succeeded)
            {
                throw new UnauthorizedAccessException("Credenciales inválidas.");
            }

            user.LastAccess = DateTime.UtcNow;
            await _unitOfWork.UserManager.UpdateAsync(user);

            var token = await CreateToken(user);

            var mappedUserDto = _mapperService.UserToUserDto(user);

            var LoggedUserDto = new LoggedUserDto
            {
                User = mappedUserDto,
                Token = token
            };

            return LoggedUserDto;
        }

        public Task Logout()
        {
            return Task.CompletedTask;
        }

        public async Task<LoggedUserDto> RegisterUser(RegisterUserDto registerUserDto)
        {
            var mappedUser = _mapperService.RegisterClientDtoToUser(registerUserDto);
            if (_unitOfWork.Users.VerifyEmail(mappedUser.Email!).Result)
            {
                throw new Exception("El email ingresado ya existe.");
            }

            var result = await _unitOfWork.Users.CreateUserAsync(mappedUser);

            if (!result)
            {
                throw new Exception("No se pudo registrar el usuario.");
            }

            await _unitOfWork.UserManager.AddToRoleAsync(mappedUser, "Cliente");

            var user = await _unitOfWork.Users.GetUserByEmail(mappedUser.Email!) ?? throw new Exception("Error del servidor, intentelo más tarde.");

            var token = await CreateToken(user);

            var mappedUserDto = _mapperService.UserToUserDto(user);

            var LoggedUserDto = new LoggedUserDto
            {
                User = mappedUserDto,
                Token = token
            };

            return LoggedUserDto;
        }

        private async Task<string> CreateToken(User user)
        {
            var roles = await _unitOfWork.UserManager.GetRolesAsync(user);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("JwtSettings:Key").Value!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}