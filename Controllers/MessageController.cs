using ChatApp.Dtos;
using ChatApp.Entities;
using ChatApp.Hubs;
using ChatApp.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace ChatApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IAppRepository _repository;
        private readonly IHubContext<ChatHub> _hubContext;
        public MessageController(IAppRepository repository, IHubContext<ChatHub> hubContext)
        {
            _repository = repository;
            _hubContext = hubContext;
        }

        [Authorize]
        [Route("addMessage")]
        [HttpPost]
        public IActionResult AddMessage(Message message)
        {
            try
            {
                MessageDto messageDto=_repository.AddMessage(message);
                _hubContext.Clients.All.SendAsync("MessageReceived", messageDto);
                return Ok();
            }
            catch (Exception ex)
            {
               return BadRequest( ex.Message);
            }
        }

        [Authorize]
        [Route("getUserMessages/{userId}/{contactId}")]
        [HttpGet]
        public IActionResult GetUserMessages(int userId, int contactId)
        {
            try
            {
                var messageList = _repository.GetUserMessages(userId, contactId);
                var messageDtoList = _repository.GetUserMessages(userId, contactId);
               return Ok( messageDtoList);
            }
            catch (Exception ex)
            {
                return BadRequest( ex.Message);
            }
        }
    }
}