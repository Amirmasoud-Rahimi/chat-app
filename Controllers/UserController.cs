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
        private readonly ILogger _logger;
        private readonly IAppRepository _repository;
        private readonly IAppService _appService;

        public UserController(IAppRepository repository, IAppService service,ILogger<UserController> logger)
        {
            _repository = repository;
            _appService = service;
            _logger = logger;
        }
       
        [Route("signUp")]
        [HttpPost]
        public IActionResult SignUp(User user)
        {
            try
            {
                _repository.SignUp(user);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,"Error Occurred in SignUp Action");
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetAllUsers()
        {
            try
            {
                var userList = _repository.GetUsers();
                var userDtoList = _appService.GetAllUsersDto(userList);
                return Ok(userDtoList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Occurred in GetAllUsers Action");
                return BadRequest(ex.Message);
            }
        }

        [Route("signIn")]
        [HttpPost]
        public IActionResult SignIn(SignInDto signInDto)
        {
            try
            {
                UserDto userDto=_repository.SignIn(signInDto.UserName, signInDto.Password);
                return Ok(userDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Occurred in SignIn Action");
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [Route("{userId}")]
        [HttpGet]
        public IActionResult GetUserById(int userId)
        {
            try
            {
                User user = _repository.GetUserById(userId);
                return user != null ? Ok(UserDto.Mapper(user)) : NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Occurred in GetUserById Action");
                return BadRequest(ex.Message);
            }
        }
    }
}