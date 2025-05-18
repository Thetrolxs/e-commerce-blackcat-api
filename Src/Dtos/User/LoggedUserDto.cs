using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace e_commerce_blackcat_api.Src.Dtos.User
{
    public class LoggedUserDto
    {
        public required UserDto User {get; set;}
        public required string Token {get; set;}
    }
}