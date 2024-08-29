using Chat.Server.Models;
using Chat.Server.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Chat.Server.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterAsync(UserModel user)
        {
            var registerResult = await _userService.RegisterAsync(user);
            if (registerResult.Succeed)
            {
                return Ok(registerResult.User);
            }
            return BadRequest(registerResult.Errors);
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> LoginAsync(LoginModel login)
        {
            var loginResult = await _userService.LoginAsync(login.Email, login.Password);
            if (loginResult.Succeed)
            {
                var user = loginResult.User;
                var claims = new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Email, user.Email),
                };
                var identity = new ClaimsIdentity(claims, "fedrated");
                var principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                return Ok(loginResult.User);
            }
            return BadRequest("login failed");
        }
    }
}
