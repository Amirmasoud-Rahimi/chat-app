using ChatApp.Dtos;
using ChatApp.Entities;
using ChatApp.Services.Interfaces;

namespace ChatApp.Services.Implements
{
    public class AppService:IAppService
    {
        public List<UserDto> GetAllUsersDto(List<User> userList)
        {
            return userList.Select(user => UserDto.Mapper(user)).ToList();
        }

        public List<MessageDto> GetAllMessagesDto(List<Message> messageList)
        {
            return messageList.Select(message => MessageDto.Mapper(message)).ToList();
        }
    }
}