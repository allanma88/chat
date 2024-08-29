using Chat.Server.Database;
using System.Text.Json.Serialization;

namespace Chat.Server.Models
{
    public class UserModel
    {
        public UserModel()
        {
        }

        public UserModel(UserEntity entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            Email = entity.Email;
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Password { get; set; }

        public string Email { get; set; }

        public UserEntity ToUserEntity()
        {
            return new UserEntity
            {
                Name = Name,
                Email = Email
            };
        }
    }
}
