using AutoMapper;
using e_commerce_blackcat_api.Src.Dtos;
using e_commerce_blackcat_api.Src.Models;
using e_commerce_blackcat_api.Src.Services.Interface;

namespace e_commerce_blackcat_api.Src.Services.Implements
{
    public class MapperService : IMapperService
    {
        private readonly IMapper _mapper;
        public MapperService(IMapper mapper){
            _mapper = mapper;
        }
        public IEnumerable<UserDto> MapUsers(IEnumerable<User> users)
        {
            var mappedUser = users.Select(u => _mapper.Map<UserDto>(u)).ToList();
            return mappedUser;
        }
        public UserDto UserToUserDto(User user)
        {
            var mappedUser = _mapper.Map<UserDto>(user);
            return mappedUser;
        }
    }
}