using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using e_commerce_blackcat_api.Src.Dtos;
using e_commerce_blackcat_api.Src.Models;

namespace e_commerce_blackcat_api.Src.Services.Interface
{
    public interface IMapperService
    {
        public IEnumerable<UserDto> MapUsers(IEnumerable<User> users);
        public UserDto UserToUserDto(User user);
        
    }
}