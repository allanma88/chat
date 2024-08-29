namespace Chat.Server.Services
{
    public interface IPasswordPolicy
    {
        IList<string> Validate(string password);
    }
}
