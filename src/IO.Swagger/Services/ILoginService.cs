using IO.Swagger.Models;
using Microsoft.AspNetCore.Http.Features.Authentication;

namespace IO.Swagger.Services
{
    public interface ILoginService
    {
        User GetUser(string Username, string Password);
        
        User Authenticate(string Username, string Password);

        void CreateUser(User user);

        void UpdateUser(User user);
    }
}