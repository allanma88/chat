using Chat.Server.Database;
using Chat.Server.Models;

namespace Chat.Server.Services
{
    public class MessageService : IMessageService
    {
        private ChatContext _context;

        public MessageService(ChatContext context)
        {
            _context = context;
        }

        public async Task AddMessageAsync(Message message)
        {
            var entity = message.ToMessageEntity();
            await _context.Messages.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public IList<string> ValidateMessage(Message message)
        {
            var errors = new List<string>();
            if (string.IsNullOrEmpty(message.Sender))
            {
                errors.Add($"{nameof(message.Sender)} is null");
            }
            if (string.IsNullOrEmpty(message.Recipient))
            {
                errors.Add($"{nameof(message.Recipient)} is null");
            }
            return errors;
        }
    }
}
