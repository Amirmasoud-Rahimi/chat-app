using ChatApp.Dtos;
using ChatApp.Entities;
using ChatApp.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IAppRepository _repository;
        private readonly IAppService _service;

        public UserController(IAppRepository repository, IAppService service)
        {
            _repository = repository;
            _service = service;
        }
       
        [Route("signUp")]
        [HttpPost]
        public IActionResult SignUp(User user)
        {
                _repository.SignUp(user);
                return Ok();    
        }

        [Route("signIn")]
        [HttpPost]
        public IActionResult SignIn(SignInDto signInDto)
        {
                UserDto userDto=_repository.SignIn(signInDto.UserName, signInDto.Password);
                return Ok(userDto);
        }

        [Authorize]
        [Route("{userId}")]
        [HttpGet]
        public IActionResult GetUserById(int userId)
        {
                User user = _repository.GetUserById(userId);
                return user != null ? Ok(UserDto.Mapper(user)) : NotFound();
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var userList = _repository.GetUsers();
            var userDtoList = _service.GetAllUsersDto(userList);
            return Ok(userDtoList);
        }
    }
}