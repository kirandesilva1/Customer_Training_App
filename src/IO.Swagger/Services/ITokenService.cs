using System;
using System.Collections.Generic;
using System.Security.Claims;
using IO.Swagger.Models;
using Microsoft.IdentityModel.Tokens;

namespace IO.Swagger.Services
{
    public interface ITokenService
    {
        string secretKey { get; set; }
        
        //void Delete();
        void SaveToken(Token token);
        
        bool IsValidToken(string tokenStr);

        string GenerateToken(Token token);

        IEnumerable<Claim> GetTokenClaims(string token);


    }
}