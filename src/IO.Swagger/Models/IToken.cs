using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace IO.Swagger.Models
{
    public interface IToken
    {
        string SecretKey { get; set; } 
        string SecurityAlgorithm { get; set; } 
        int ExpireMinutes { get; set; }
        Claim[] Claims { get; set; }
    }
}