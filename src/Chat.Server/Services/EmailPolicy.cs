using Chat.Server.Database;
using System.ComponentModel.DataAnnotations;

namespace Chat.Server.Services
{
    public class EmailPolicy : IEmailPolicy
    {
        private ChatContext _context;

        public EmailPolicy(ChatContext context)
        {
            _context = context;
        }

        public IList<string> Validate(string email)
        {
            var errors = new List<string>();

            if (!new EmailAddressAttribute().IsValid(email))
            {
                errors.Add($"invalid email: {email}");
            }

            if (_context.Users.Any(u => u.Email == email))
            {
                errors.Add($"duplicated email: {email}");
            }

            return errors;
        }
    }
}
