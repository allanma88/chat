namespace Chat.Server.Models
{
    public class LoginResult
    {
        public bool Succeed { get; set; }

        public UserModel User { get; set; }
    }
}
