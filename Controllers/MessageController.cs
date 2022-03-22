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
        private readonly IAppService _service;
        private readonly IHubContext<ChatHub> _hubContext;
        public MessageController(IAppRepository repository, IAppService service, IHubContext<ChatHub> hubContext)
        {
            _repository = repository;
            _service = service;
            _hubContext = hubContext;
        }

        [Authorize]
        [Route("addMessage")]
        [HttpPost]
        public IActionResult AddMessage(Message message)
        {
                Message m=_repository.AddMessage(message);
                MessageDto messageDto = MessageDto.Mapper(m);
                _hubContext.Clients.All.SendAsync("MessageReceived", messageDto);
                return Ok();
        }

        [Authorize]
        [Route("getUserMessages/{userId}/{contactId}")]
        [HttpGet]
        public IActionResult GetUserMessages(int userId, int contactId)
        {
                var messageList = _repository.GetUserMessages(userId, contactId);
                var messageDtoList = _service.GetAllMessagesDto(messageList);
               return Ok( messageDtoList);
        }
    }
}