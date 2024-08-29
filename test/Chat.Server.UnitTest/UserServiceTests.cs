using Chat.Server.Database;
using Chat.Server.Models;
using Chat.Server.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Chat.Server.UnitTest
{
    public class UserServiceTests
    {
        private ChatContext _chatContext;
        private PasswordPolicy _passwordPolicy;
        private PasswordHasher<UserEntity> _passwordHasher;

        [SetUp]
        public async Task Setup()
        {
            _passwordPolicy = new PasswordPolicy(new PasswordOptions());
            _passwordHasher = new PasswordHasher<UserEntity>();

            var options = new DbContextOptionsBuilder<ChatContext>().UseInMemoryDatabase("Chat").Options;
            _chatContext = new ChatContext(options);
        }

        [Test]
        public async Task Login_Test()
        {
            var userService = new UserService(_passwordPolicy, _chatContext);

            var userModel = new UserModel
            {
                Name = "user1",
                Email = "user1@company.com",
                Password = "password1"
            };
            var userEntity = userModel.ToUserEntity();
            userEntity.PasswordHash = _passwordHasher.HashPassword(userEntity, userModel.Password);
            var addedEntry = await _chatContext.Users.AddAsync(userEntity);
            await _chatContext.SaveChangesAsync();

            var loginResult = await userService.LoginAsync(userModel.Email, userModel.Password);

            Assert.NotNull(loginResult.User);
            Assert.That(loginResult.User.Id, Is.EqualTo(addedEntry.Entity.Id));
        }

        [Test]
        public async Task Register_Login_Test()
        {
            var userService = new UserService(_passwordPolicy, _chatContext);
            var userModel = new UserModel
            {
                Name = "user2",
                Email = "user2@company.com",
                Password = "Pass#word2"
            };
            var registerResult = await userService.RegisterAsync(userModel);

            Assert.Zero(registerResult.Errors.Count);

            var loginResult = await userService.LoginAsync(userModel.Email, userModel.Password);

            Assert.NotNull(loginResult.User);
        }
    }
}