using ChatApp.Entities;

namespace ChatApp.Services.Interfaces
{
    public interface IAppRepository
    {
        List<User> GetUsers(); 
    }
}
