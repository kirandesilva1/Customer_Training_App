using IO.Swagger.Models;
using IO.Swagger.Services;

namespace IO.Swagger.BusinessLayer
{
    public class LoginManager : ILoginManager
    {
        private readonly ILoginService _loginService;

        public LoginManager(ILoginService loginService)
        {
            _loginService = loginService;
        }
        
        public User Authenticate(string Username, string Password)
        {
          return  _loginService.Authenticate(Username, Password);
        }

        public void CreateLogin(User user)
        {
            _loginService.CreateUser(user);
        }
    }
}