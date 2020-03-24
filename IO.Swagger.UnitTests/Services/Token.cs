using System;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using IO.Swagger.Helpers;
using IO.Swagger.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IO.Swagger.UnitTests.Services
{
    [TestClass]
    public class Token
    {
        private IOptions<AppSettings> _config;
        
        [TestMethod]
        public void Should_Create_Token()
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false)
                .Build();

            _config = Options.Create(configuration.GetSection("AppSettings").Get<AppSettings>());
            TokenService tokenService = new TokenService(_config,null);
            SecurityToken _securityToken = tokenService.Create("TestToken", DateTime.UtcNow.AddDays(7));
            
            Assert.AreNotEqual("TestToken",tokenHandler.WriteToken(_securityToken));
            
        }
    }
}