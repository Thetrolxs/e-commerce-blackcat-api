using e_commerce_blackcat_api.Src.Dtos;
using e_commerce_blackcat_api.Src.Dtos.User;
using e_commerce_blackcat_api.Src.Models;

namespace e_commerce_blackcat_api.Src.Services.Interface
{
    public interface IMapperService
    {
        public IEnumerable<UserDto> MapUsers(IEnumerable<User> users);
        public UserDto UserToUserDto(User user);
        public User UserDtoToUser(UserDto userDto);
        public List<UserPageDto> UserToUserPage(List<User> users);

    }
}