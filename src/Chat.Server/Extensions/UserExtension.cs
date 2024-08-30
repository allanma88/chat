using Chat.Server.Services;
using Microsoft.AspNetCore.Identity;

namespace Chat.Server.Extensions
{
    public static class UserExtension
    {
        public static void AddUserService(this IServiceCollection services, Action<PasswordOptions> configure)
        {
            var passwordOptions = new PasswordOptions();
            configure(passwordOptions);
            var passwordPolicy = new PasswordPolicy(passwordOptions);
            services.AddSingleton<IPasswordPolicy>(passwordPolicy);

            services.AddScoped<IUserService, UserService>();
        }
    }
}
