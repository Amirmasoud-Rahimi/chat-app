using ChatApp.Dtos;
using ChatApp.Entities;

namespace ChatApp.Services.Interfaces
{
    public interface IAppService
    {
        List<UserDto> GetAllUsersDto(List<User> userList);
        List<MessageDto> GetAllMessagesDto(List<Message> messageList);
    }
}