using ChatApp.Entities;

namespace ChatApp.Dtos
{
    public class UserDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public DateTime DateOfJoining { get; set; }
        public string PhotoFileName { get; set; }
        public string Token { get; set; }

        public static UserDto Mapper(User user)
        {
            return new UserDto
            {
                Id = user.UserId,
                UserName = user.UserName,
                FullName = user.FullName,
                DateOfJoining = user.DateOfJoining,
                PhotoFileName = user.PhtotFileName
            };
        }

        public static UserDto SignInMapper(User user, String token)
        {
            UserDto userDto = UserDto.Mapper(user);
            userDto.Token = token;
            return userDto;
        }
    }
}