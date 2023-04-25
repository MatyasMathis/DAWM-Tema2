using DataLayer.Dtos;
using DataLayer.Entities;


namespace DataLayer.Mapping
{
    public static class UserMappingExtension
    {
        public static List<UserDto> ToUserDtos(this List<User> users)
        {
            if (users == null)
            {
                return null;
            }

            var results = users.Select(e => e.ToUserDto()).ToList();

            return results;
        }

        public static UserDto ToUserDto(this User user)
        {
            if (user == null) return null;

            var result = new UserDto();
            result.UserName = user.UserName;
            result.Password = user.Password;

            return result;
        }
    }
}
