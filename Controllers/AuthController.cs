using BCrypt.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MovieBackAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MovieBackAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AuthController : ControllerBase
    {
        private readonly Context dbContext;
        private readonly IConfiguration configuration;

        public AuthController(Context context, IConfiguration configuration)
        {
            this.dbContext = context;
            this.configuration = configuration; 
        }

        /// <summary>
        /// Create a user with a Name and a Password
        /// </summary>
        [HttpPost]
        public IActionResult Register([FromBody] AddUserDTO user)
        {
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(user.Password);

            var userFound = dbContext.Users.FirstOrDefault(x => x.Name == user.UserName);
            if (userFound != null)
            {
                return BadRequest("User already exist for this name.");
            }

            var newUser = new User
            {
                Name = user.UserName,
                Password = passwordHash
            };

            dbContext.Users.Add(newUser);
            dbContext.SaveChanges();

            return Ok(newUser);
        }

        /// <summary>
        /// Connect the user from his login and password and create a JWT token
        /// </summary>
        /// <returns>JWT token to be authentificate</returns>
        [HttpPost]
        public IActionResult Login([FromBody] UserDTO loginUser)
        {
            var user = dbContext.Users.FirstOrDefault(x => x.Name == loginUser.Name);
            if(user == null)
            {
                return BadRequest("User not found.");
            }

            if(!BCrypt.Net.BCrypt.Verify(loginUser.Password, user.Password))
            {
                return BadRequest("User not found.");
            }

            string token = CreateJwtToken(user);

            return Ok(new { user, token });
        }

        private string CreateJwtToken(User user)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.Name),
                new Claim("userId", user.Id.ToString())
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                configuration.GetSection("JwtKey").Value!));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddDays(30),
                    signingCredentials: cred
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}