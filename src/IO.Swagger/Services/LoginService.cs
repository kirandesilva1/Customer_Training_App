using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using IO.Swagger.Data;
using IO.Swagger.Helpers;
using IO.Swagger.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace IO.Swagger.Services
{
    public class LoginService : ILoginService
    {
        private readonly AppSettings _appSettings;
        private readonly IRepository<User> _userRepository;
        
        public LoginService(IOptions<AppSettings> appSettings,IRepository<User> userRepository)
        {
            _appSettings = appSettings.Value;
            _userRepository = userRepository;
        }
        
        public User GetUser(string Username, string Password)
        {
            return  _userRepository.Query(x => x.Username == Username && x.Password == Password).First();
        }

        public User Authenticate(string Username, string Password)
        {
            var user = GetUser(Username,Password);
            
            if (user == null)
                return null;

            //user = SetToken(user);
            return user;

        }

        public void CreateUser(User user)
        {
            //SetToken(user);
            _userRepository.Create(user);
        }

        public void UpdateUser(User user)
        {
            _userRepository.Update(user);
        }

        // private User SetToken(User user)
        // {
        //     var tokenHandler = new JwtSecurityTokenHandler();
        //     var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
        //     var tokenDescriptor = new SecurityTokenDescriptor
        //     {
        //         Subject = new ClaimsIdentity(new Claim[] 
        //         {
        //             new Claim(ClaimTypes.Name, user.Id.ToString())
        //         }),
        //         Expires = DateTime.UtcNow.AddDays(7),
        //         SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        //     };
        //     var token = tokenHandler.CreateToken(tokenDescriptor);
        //     user.Token = tokenHandler.WriteToken(token);
        //     
        //     user.Password = null;
        //
        //     return user;
        // }
        
    }
}