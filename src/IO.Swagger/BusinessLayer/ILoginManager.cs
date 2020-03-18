using IO.Swagger.Models;

namespace IO.Swagger.BusinessLayer
{
    public interface ILoginManager
    {
        User Authenticate(string Username, string Password);
    }
}