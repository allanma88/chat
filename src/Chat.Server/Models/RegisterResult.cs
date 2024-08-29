namespace Chat.Server.Models
{
    public class RegisterResult
    {
        public bool Succeed
        {
            get { return Errors.Count == 0; }
        }

        public IList<string> Errors { get; set; } = new List<string>();

        public UserModel User { get; set; }
    }
}
