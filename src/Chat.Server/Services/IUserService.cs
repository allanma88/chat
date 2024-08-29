using Chat.Server.Models;

namespace Chat.Server.Services
{
    public interface IUserService
    {
        Task<LoginResult> LoginAsync(string name, string password);

        public Task<RegisterResult> RegisterAsync(UserModel user);
    }
}
