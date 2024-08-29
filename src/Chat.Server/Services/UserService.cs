using Chat.Server.Database;
using Chat.Server.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Chat.Server.Services
{
    public class UserService : IUserService
    {
        private IPasswordPolicy _passwordPolicy;
        private IEmailPolicy _emailPolicy;
        private ChatContext _context;
        private IPasswordHasher<UserEntity> _passwordHasher;

        public UserService(IPasswordPolicy passwordPolicy, ChatContext context)
        {
            _passwordPolicy = passwordPolicy;
            _emailPolicy = new EmailPolicy(context);
            _context = context;
            _passwordHasher = new PasswordHasher<UserEntity>();
        }

        public async Task<RegisterResult> RegisterAsync(UserModel user)
        {
            var errors = ValidateUser(user);
            if (errors.Count > 0)
            {
                return new RegisterResult { Errors = errors };
            }

            var userEntity = user.ToUserEntity();
            var passwordHash = _passwordHasher.HashPassword(userEntity, user.Password);
            userEntity.PasswordHash = passwordHash;
            var addedEntry = await _context.Users.AddAsync(userEntity);
            await _context.SaveChangesAsync();

            return new RegisterResult { User = new UserModel(addedEntry.Entity) };
        }

        public async Task<LoginResult> LoginAsync(string email, string password)
        {
            var passwordHash = _passwordHasher.HashPassword(null, password);

            var userEntity = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

            if (userEntity != null)
            {
                var verifyResult = _passwordHasher.VerifyHashedPassword(userEntity, userEntity.PasswordHash, password);
                if (verifyResult == PasswordVerificationResult.Success)
                {
                    return new LoginResult { Succeed = true, User = new UserModel(userEntity) };
                }
            }
            return new LoginResult { Succeed = false };
        }

        private IList<string> ValidateUser(UserModel user)
        {
            var errors = new List<string>();

            var emailErrors = _emailPolicy.Validate(user.Email);
            errors.AddRange(emailErrors);

            var passwordErrors = _passwordPolicy.Validate(user.Password);
            errors.AddRange(passwordErrors);

            return errors;
        }
    }
}
