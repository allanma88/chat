using Chat.Server.Hubs;
using Chat.Server.Models;
using Chat.Server.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Chat.Server.Controllers
{
    //TODO: we should support token-based authorize, but here for simplicity, we just bypass the authorize
    [Route("api/[controller]")]
    //[Authorize]
    [ApiController]
    public class MessagesController : Controller
    {
        private IHubContext<ChatHub> _hubContext;
        private IMessageService _messageService;

        public MessagesController(IHubContext<ChatHub> hubContext, IMessageService messageService)
        {
            _hubContext = hubContext;
            _messageService = messageService;
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendAsync(Message msg)
        {
            var errors = _messageService.ValidateMessage(msg);
            if (errors.Count > 0)
            {
                return BadRequest(errors);
            }

            await _hubContext.Clients.User(msg.Recipient).SendAsync("onReceive", msg);
            await _messageService.AddMessageAsync(msg);
            return Ok("succeed");
        }
    }
}
