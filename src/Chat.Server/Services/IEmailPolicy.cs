namespace Chat.Server.Services
{
    public interface IEmailPolicy
    {
        IList<string> Validate(string email);
    }
}
