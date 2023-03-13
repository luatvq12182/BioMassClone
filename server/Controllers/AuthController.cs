using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using server.DataAccess.Entities;
using server.Options;
using server.Services;
using server.ViewModel.Users;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BC = BCrypt.Net.BCrypt;

namespace server.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _config;
        public AuthController(IUserService userService , IConfiguration config)
        {
            _userService = userService;
            _config = config;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginModel model) 
        {
            IActionResult response = Unauthorized();

            var user = await AuthenticateUser(model);

            if (user != null)
            {
                var tokenString = GenerateJSONWebToken(user);
                response = Ok(new { token = tokenString, userName = user.UserName, email = user.Email });
            }

            return response;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                if(model.Password != model.RePassword)
                {
                    return BadRequest("");
                }
                if (_userService.AlreadyExist(model, out string message))
                {
                    return BadRequest(message);
                }
                else
                {
                    var user = new User
                    {
                        Email = model.Email,
                        Password = BC.HashPassword(model.Password),
                        UserName = model.UserName,
                        IsAdmin = true
                    };
                    await _userService.Insert(user);

                    return Ok();
                }
            }
            return BadRequest();
        }
        private async Task<UserModel> AuthenticateUser(LoginModel login)
        {
            var user = await _userService.GetUserByIdentify(login);
            if (user != null)
            {
                return new UserModel
                {
                    UserName= user.UserName,
                    Email = user.Email,
                    IsAdmin = user.IsAdmin
                };
            }
            return null;

        }
        private string GenerateJSONWebToken(UserModel userInfo)
        {
            var jwtOptions = new JwtOption();
            _config.GetSection("Jwt").Bind(jwtOptions);
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Name, userInfo.UserName),
                new Claim(JwtRegisteredClaimNames.Email, userInfo.Email)
            };

            var token = new JwtSecurityToken(jwtOptions.Issuer,
                jwtOptions.Audience,
                claims,
                expires: DateTime.Now.AddMinutes(1200),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
