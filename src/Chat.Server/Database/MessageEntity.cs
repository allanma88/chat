using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Chat.Server.Database
{
    [Table("Message")]
    public class MessageEntity
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Sender { get; set; }

        [Required]
        public string Recipient { get; set; }

        [Required]
        public DateTime Timestamp { get; set; }

        [Required]
        public string Content { get; set; }
    }
}
