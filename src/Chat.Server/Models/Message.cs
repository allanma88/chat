using Chat.Server.Database;
using System.Text.Json;

namespace Chat.Server.Models
{
    public class Message
    {
        public string Sender { get; set; }

        public string Recipient { get; set; }

        public DateTime Timestamp { get; set; }

        public object Content { get; set; }

        public MessageEntity ToMessageEntity()
        {
            var content = JsonSerializer.Serialize(Content);
            return new MessageEntity
            {
                Sender = Sender,
                Recipient = Recipient,
                Timestamp = Timestamp.ToUniversalTime(),
                Content = content,
            };
        }
    }
}
