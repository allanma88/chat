using Microsoft.AspNetCore.SignalR;

namespace Chat.Server.Hubs
{
    public class UserIdProvider : IUserIdProvider
    {
        public string? GetUserId(HubConnectionContext connection)
        {
            var context = connection.GetHttpContext();
            var userId = context.Request.Query["userid"];
            return userId;
        }
    }
}
