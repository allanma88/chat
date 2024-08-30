using Chat.Server.Models;

namespace Chat.Server.Services
{
    public interface IMessageService
    {
       Task AddMessageAsync(Message message);

        IList<string> ValidateMessage(Message message);
    }
}
