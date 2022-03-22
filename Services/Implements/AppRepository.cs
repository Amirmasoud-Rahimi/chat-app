using ChatApp.Dao;
using ChatApp.Dtos;
using ChatApp.Entities;
using ChatApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using BCryptTool = BCrypt.Net.BCrypt;

namespace ChatApp.Services.Implements
{
    public class AppRepository:IAppRepository
    {
        private readonly AppDbContext _context;
        private readonly JwtManager _jwtManager;

        public AppRepository(AppDbContext context,JwtManager jwtManager)
        {
            _context = context;
            _jwtManager = jwtManager;
        }

        public List<User> GetUsers() => _context.Users.ToList();

        public User GetUserById(int id) => _context.Users.SingleOrDefault(user => user.UserId == id);

        public User GetUserByUserName(string userName) => _context.Users.SingleOrDefault(user => user.UserName == userName);

        public void SignUp(User user)
        {
                string salt = BCryptTool.GenerateSalt();
                user.Password = BCryptTool.HashPassword(user.Password, salt);
                user.DateOfJoining = DateTime.Now;
                _context.Users.Add(user);
                _context.SaveChanges();
        }

        public UserDto SignIn(string userName, string password)
        {
            var user = GetUserByUserName(userName);

            if (user != null && BCryptTool.Verify(password, user.Password))
            {        
                string token = "Bearer "+_jwtManager.GenerateToken(user);
                UserDto dto = UserDto.SignInMapper(user, token);
                return dto;
            }
            throw new KeyNotFoundException("user not found");
        }

        public Message AddMessage(Message message)
        {
            message.SendingDate = DateTime.Now;
            EntityEntry<Message> messageEntity= _context.Messages.Add(message);
            _context.SaveChanges();
            return messageEntity.Entity;
        }

        public List<Message> GetUserMessages(int userId, int contactId)
        {
            List<Message> messages = _context.Messages.Where(
                message => (message.SenderId == userId
                && message.ReceiverId == contactId) ||
                (message.SenderId == contactId
                && message.ReceiverId == userId)
                ).ToList();
            return messages;
        }
    }
}