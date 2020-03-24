using System;
using Microsoft.IdentityModel.Tokens;

namespace IO.Swagger.Services
{
    public interface ITokenService
    {
        //SecurityToken Get(); 
        SecurityToken Create(string tokenStr, DateTime expirationDate);
        //void Delete();
        //Save();
    }
}