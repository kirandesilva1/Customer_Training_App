using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using IO.Swagger.Data;
using IO.Swagger.Helpers;
using IO.Swagger.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace IO.Swagger.Services
{
    public class TokenService : ITokenService
    {
        private readonly AppSettings _appSettings;
        private readonly IRepository<Token> _tokenRepository;
        
        public TokenService(IOptions<AppSettings> appSettings,IRepository<Token> tokenRepository)
        {
            _appSettings = appSettings.Value;
            _tokenRepository = tokenRepository;
        }
        
        public SecurityToken Create(string tokenStr, DateTime expirationDate)
        {
             var tokenHandler = new JwtSecurityTokenHandler(); 
             var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
             var tokenDescriptor = new SecurityTokenDescriptor
             {
                            Subject = new ClaimsIdentity(new Claim[] 
                            {
                                new Claim(ClaimTypes.Name, tokenStr)
                            }),
                            Expires = DateTime.UtcNow.AddDays(7),
                            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
             };
            return tokenHandler.CreateToken(tokenDescriptor);
        }
    }
}