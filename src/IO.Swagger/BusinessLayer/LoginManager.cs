using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using IO.Swagger.Helpers;
using IO.Swagger.Models;
using IO.Swagger.Services;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace IO.Swagger.BusinessLayer
{
    public class LoginManager : ILoginManager
    {
        private readonly ILoginService _loginService;
        private readonly ITokenService _tokenService;
        private readonly AppSettings _appSettings;

        public LoginManager(AppSettings appSettings, ILoginService loginService, ITokenService tokenService)
        {
            _loginService = loginService;
            _tokenService = tokenService;
            _appSettings = appSettings;
        }

        public User Authenticate(string Username, string Password)
        {
            User user = _loginService.Authenticate(Username, Password);

            if (user.Token is null)
            {
                UpdateUserToken(user);
            }
            else
            {
                if (!_tokenService.IsValidToken(user.Token)) return user;
                if (IsTokenExpired(user))
                {
                    UpdateUserToken(user);
                }
            }

            return user;
        }

        private void UpdateUserToken(User user)
        {
            Token tempToken = LoadToken(user);
            user.Token = _tokenService.GenerateToken(tempToken);
            _tokenService.SaveToken(tempToken);
            _loginService.UpdateUser(user);
        }

        private bool IsTokenExpired(User user)
        {
            List<Claim> tokenClaims = _tokenService.GetTokenClaims(user.Token).ToList();

            DateTime parsedDate =
                DateTime.Parse(tokenClaims.FirstOrDefault(e => e.Type.Equals(ClaimTypes.Expiration)).Value);

            if (parsedDate > DateTime.Today.Date)
            {
                return true;
            }

            return false;
        }

        private Token LoadToken(User user)
        {
            Token token = new Token();
            token.ExpireMinutes = 1440; //TODO: Move to appsettings file
            token.SecretKey = _appSettings.Secret;
            token.SecurityAlgorithm = SecurityAlgorithms.HmacSha256Signature;
            token.Claims = new Claim[]
            {
                new Claim(ClaimTypes.Email, user.Emailaddress),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Expiration,
                    DateTime.UtcNow.Add(TimeSpan.FromMinutes(token.ExpireMinutes)).ToString())
            };

            return token;
        }

        public void CreateLogin(User user)
        {
            _loginService.CreateUser(user);
        }
    }
}