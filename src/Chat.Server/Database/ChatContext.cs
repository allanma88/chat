using Microsoft.EntityFrameworkCore;

namespace Chat.Server.Database
{
    public class ChatContext : DbContext
    {
        public DbSet<UserEntity> Users { get; set; }

        public DbSet<MessageEntity> Messages { get; set; }

        public ChatContext(DbContextOptions<ChatContext> options) : base(options)
        {
        }
    }
}