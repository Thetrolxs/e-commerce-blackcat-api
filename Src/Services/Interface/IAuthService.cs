using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using e_commerce_blackcat_api.Src.Dtos.User;

namespace e_commerce_blackcat_api.Src.Services.Interface
{
    public interface IAuthService
    {
        Task<LoggedUserDto> RegisterUser(RegisterUserDto registerUserDto);
        Task<LoggedUserDto> Login(LoginUserDto loginUserDto);
    }
}