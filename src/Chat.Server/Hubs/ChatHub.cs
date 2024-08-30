using Chat.Server.Models;
using Chat.Server.Services;
using Microsoft.AspNetCore.SignalR;
using System.Text.Json;

namespace Chat.Server.Hubs
{
    public class ChatHub : Hub
    {
        private IMessageService _messageService;

        public ChatHub(IMessageService messageService)
        {
            _messageService = messageService;
        }

        public override Task OnConnectedAsync()
        {
            Console.WriteLine($"{Context.UserIdentifier} connected at {Context.ConnectionId}");
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            Console.WriteLine($"{Context.UserIdentifier} disconnected at {Context.ConnectionId}");
            return base.OnDisconnectedAsync(exception);
        }

        [HubMethodName("Send")]
        public async Task SendAsync(Message msg)
        {
            var errors = _messageService.ValidateMessage(msg);
            if(errors.Count > 0)
            {
                var error = JsonSerializer.Serialize(errors);
                throw new HubException(error);
            }

            await Clients.User(msg.Recipient).SendAsync("onReceive", msg);
            await _messageService.AddMessageAsync(msg);
        }
    }
}
