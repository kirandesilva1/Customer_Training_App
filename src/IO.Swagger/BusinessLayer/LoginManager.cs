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

            //Load token parameters
            Token token = new Token();
            token.ExpireMinutes = 1440; //TODO: Move to appsettings file
            token.SecretKey = _appSettings.Secret;
            token.SecurityAlgorithm = SecurityAlgorithms.HmacSha256Signature;
            token.Claims = new Claim[]
            {
                new Claim(ClaimTypes.Email, user.Emailaddress),
                new Claim(ClaimTypes.Name, user.Username),
            };

            user.Token = _tokenService.GenerateToken(token);

            _tokenService.SaveToken(token);
            
            _loginService.UpdateUser(user);

            return user;
        }

        public void CreateLogin(User user)
        {
            _loginService.CreateUser(user);
        }
    }
}