using ChatApp.Dtos;
using ChatApp.Entities;

namespace ChatApp.Services.Interfaces
{
    public interface IAppRepository
    {
        void SignUp(User user);
        List<User> GetUsers();
        UserDto SignIn(string userName, string password);
        User GetUserById(int id);
        User GetUserByUserName(string userName);
        Message AddMessage(Message message);
        List<Message> GetUserMessages(int userId, int contactId);
    }
}