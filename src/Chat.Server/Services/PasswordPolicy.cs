using Microsoft.AspNetCore.Identity;

namespace Chat.Server.Services
{
    public class PasswordPolicy : IPasswordPolicy
    {
        private PasswordOptions _options;

        public PasswordPolicy(PasswordOptions options)
        {
            _options = options;
        }

        public IList<string> Validate(string password)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(password) || password.Length < _options.RequiredLength)
            {
                errors.Add($"Password too short: {_options.RequiredLength}");
            }
            if (_options.RequireNonAlphanumeric && password.All(IsLetterOrDigit))
            {
                errors.Add("Password requires non alphanumeric");
            }
            if (_options.RequireDigit && !password.Any(IsDigit))
            {
                errors.Add("Password requires digit");
            }
            if (_options.RequireLowercase && !password.Any(IsLower))
            {
                errors.Add("Password requires lower");
            }
            if (_options.RequireUppercase && !password.Any(IsUpper))
            {
                errors.Add("Password requires upper");
            }
            if (_options.RequiredUniqueChars >= 1 && password.Distinct().Count() < _options.RequiredUniqueChars)
            {
                errors.Add($"Password requires unique chars: {_options.RequiredUniqueChars}");
            }
            return errors;
        }

        private bool IsDigit(char c)
        {
            return c >= '0' && c <= '9';
        }
        
        private bool IsLower(char c)
        {
            return c >= 'a' && c <= 'z';
        }

        private bool IsUpper(char c)
        {
            return c >= 'A' && c <= 'Z';
        }

        private bool IsLetterOrDigit(char c)
        {
            return IsUpper(c) || IsLower(c) || IsDigit(c);
        }
    }
}
