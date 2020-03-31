using System;
using System.Collections.Generic;
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
        public string secretKey { get; set; }
        
        public TokenService(IOptions<AppSettings> appSettings,IRepository<Token> tokenRepository)
        {
            _appSettings = appSettings.Value;
            _tokenRepository = tokenRepository;
        }

        private SecurityKey GetSymmetricSecurityKey()
        {
            byte[] symmetricKey = Convert.FromBase64String(_appSettings.Secret);
            return new SymmetricSecurityKey(symmetricKey);
        }
        
        private TokenValidationParameters GetTokenValidationParameters()
        {
            return new TokenValidationParameters()
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                IssuerSigningKey = GetSymmetricSecurityKey()
            };
        }
        
        public void SaveToken(Token token)
        {
            _tokenRepository.Create(token);
        }

        public bool IsValidToken(string tokenStr)
        {
            if (string.IsNullOrEmpty(tokenStr))
                throw new ArgumentException("Given token is null or empty.");

            TokenValidationParameters tokenValidationParameters = GetTokenValidationParameters();

            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            
            try
            {
                ClaimsPrincipal tokenValid = jwtSecurityTokenHandler.ValidateToken(tokenStr, tokenValidationParameters, out SecurityToken validatedToken);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public string GenerateToken(Token token)
        {
            var tokenHandler = new JwtSecurityTokenHandler(); 
            //var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(token.Claims),
                Expires = DateTime.UtcNow.AddMinutes(Convert.ToInt32(token.ExpireMinutes)),
                SigningCredentials = new SigningCredentials(GetSymmetricSecurityKey(), token.SecurityAlgorithm)
                // Subject = new ClaimsIdentity(new Claim[] 
                // {
                //     new Claim(ClaimTypes.Name, tokenStr)
                // }),
                // Expires = DateTime.UtcNow.AddDays(7),
                // SigningCredentials = new SigningCredentials(GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256Signature)
            };
             
            SecurityToken securityToken = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(securityToken);
        }

        public IEnumerable<Claim> GetTokenClaims(string token)
        {
            if (string.IsNullOrEmpty(token))
                throw new ArgumentException("Token is null or empty.");

            TokenValidationParameters tokenValidationParameters = GetTokenValidationParameters();

            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            try
            {
                ClaimsPrincipal tokenValid = jwtSecurityTokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken validatedToken);
                return tokenValid.Claims;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}