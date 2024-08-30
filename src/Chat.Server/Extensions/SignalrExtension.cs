using Chat.Server.Hubs;
using Chat.Server.Services;
using Microsoft.AspNetCore.SignalR;

namespace Chat.Server.Extensions
{
    public static class SignalrExtension
    {
        public static void AddSignalrService(this IServiceCollection services)
        {
            services.AddSignalR();

            services.AddSingleton<IUserIdProvider, UserIdProvider>();

            services.AddScoped<IMessageService, MessageService>();

        }
    }
}
