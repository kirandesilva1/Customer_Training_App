using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using IO.Swagger.Helpers;
using IO.Swagger.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace IO.Swagger.Services
{
    public class LoginService : ILoginService
    {
        private readonly AppSettings _appSettings;
        
        public LoginService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }
        
        public User GetUser(string Username, string Password)
        {
            List<User> users = new List<User>
            {
                new User
                {
                    Id = new System.Guid(),
                    Username = "Kiran1",
                    Password = "Password@123"
                }
            };

            return users.Find(x => x.Username == Username && x.Password == Password);

        }

        public User Authenticate(string Username, string Password)
        {
            var user = GetUser(Username,Password);
            
            if (user == null)
                return null;

            user = SetToken(user);
            return user;

        }

        private User SetToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] 
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);
            
            user.Password = null;

            return user;
        }
        
    }
}