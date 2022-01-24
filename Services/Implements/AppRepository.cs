using ChatApp.Dao;
using ChatApp.Entities;
using ChatApp.Services.Interfaces;

namespace ChatApp.Services.Implements
{
    public class AppRepository:IAppRepository
    {
        private readonly AppDbContext _context;
        public AppRepository(AppDbContext context)// dependency injection
        {
            _context = context;
        }
        public List<User> GetUsers()
        {
            return _context.Users.ToList();
        }
    }
}
