using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using IO.Swagger.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Xunit;

namespace IO.Swagger.UnitTests.Functional
{
    public class LoginControllerTests  : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public LoginControllerTests(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Should_Not_Permit_User_To_Login_With_Only_UserName()
        {
            var client = _factory.CreateClient();
            
            User login = new User();
            login.Username = "User1";
            login.Password = "";

            string JSON = JsonConvert.SerializeObject(login);
            var httpContent = new StringContent(JSON, Encoding.UTF8, "application/json");
            var loginResponse = await client.PostAsync("/v1/login", httpContent);
            
            Assert.Equal(HttpStatusCode.BadRequest,loginResponse.StatusCode);

        }
        
        [Fact]
        public async Task Should_Not_Permit_User_To_Login_With_Only_Password()
        {
            var client = _factory.CreateClient();
            
            User login = new User();
            login.Username = "";
            login.Password = "Password@12345";

            string JSON = JsonConvert.SerializeObject(login);
            var httpContent = new StringContent(JSON, Encoding.UTF8, "application/json");
            var loginResponse = await client.PostAsync("/v1/login", httpContent);
            
            Assert.Equal(HttpStatusCode.BadRequest,loginResponse.StatusCode);
        }
        
        
        [Fact]
        public async Task Should_Not_Permit_Invalid_User_To_Login()
        {
            var client = _factory.CreateClient();
            
            User login = new User();
            login.Username = "User1";
            login.Password = "Password@12345";

            string JSON = JsonConvert.SerializeObject(login);
            var httpContent = new StringContent(JSON, Encoding.UTF8, "application/json");
            var loginResponse = await client.PostAsync("/v1/login", httpContent);
            
            Assert.Equal(HttpStatusCode.BadRequest,loginResponse.StatusCode);
        }
        
        [Fact]
        public async Task Should_Let_User_With_Valid_Credentials_To_Login()
        {
            var client = _factory.CreateClient();
            
            User login = new User();
            login.Username = "Kiran1";
            login.Password = "Password@123";

            string JSON = JsonConvert.SerializeObject(login);
            var httpContent = new StringContent(JSON, Encoding.UTF8, "application/json");
            var loginResponse = await client.PostAsync("/v1/login", httpContent);
            
            Assert.Equal(HttpStatusCode.OK,loginResponse.StatusCode);
        }
        
    }
}