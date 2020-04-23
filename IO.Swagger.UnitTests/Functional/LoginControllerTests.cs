using System;
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
        
        public string RandomString(int size, bool lowerCase)  
        {  
            StringBuilder builder = new StringBuilder();  
            Random random = new Random();  
            char ch;  
            for (int i = 0; i < size; i++)  
            {  
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));  
                builder.Append(ch);  
            }  
            if (lowerCase)  
                return builder.ToString().ToLower();  
            return builder.ToString();  
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
            
            string randomUser = "User" + this.RandomString(5,true);
            string randomEmail = "Test" + this.RandomString(6,true) + "@yahoo.com";
            string randomPassword = "Password@" + this.RandomString(9,true);
            
            // Create random user
            User user = new User();
            user.Username = randomUser;
            user.Emailaddress = randomEmail;
            user.Firstname = "Tom";
            user.Lastname = "Thumb";
            user.Password = randomPassword;
            //user.Token = "TokenTest";
            user.Phonenumber = "412-567-8900";
            
            string userJSON = JsonConvert.SerializeObject(user);
            var createHttpContent = new StringContent(userJSON, Encoding.UTF8, "application/json");
            var createUserResponse = await client.PostAsync("/v1/create", createHttpContent);
            
            Assert.Equal(HttpStatusCode.OK,createUserResponse.StatusCode);
            
            Login loginUser = new Login();
            loginUser.Username = randomUser;
            loginUser.Password = randomPassword;

            string JSON = JsonConvert.SerializeObject(loginUser);
            var httpContent = new StringContent(JSON, Encoding.UTF8, "application/json");
            var loginResponse = await client.PostAsync("/v1/login", httpContent);
            
            Assert.Equal(HttpStatusCode.OK,loginResponse.StatusCode);
        }


        [Fact]
        public async Task Should_Create_A_User()
        {
            var client = _factory.CreateClient();
            
            User user = new User();
            user.Username = "User" + this.RandomString(5,true);
            user.Emailaddress = "Test" + this.RandomString(6,true) + "@yahoo.com";
            user.Firstname = "Tom";
            user.Lastname = "Thumb";
            user.Password = "Password@" + this.RandomString(9,true);
            //user.Token = "TokenTest";
            user.Phonenumber = "412-567-8900";
            
            string JSON = JsonConvert.SerializeObject(user);
            var httpContent = new StringContent(JSON, Encoding.UTF8, "application/json");
            var loginResponse = await client.PostAsync("/v1/create", httpContent);
            
            Assert.Equal(HttpStatusCode.OK,loginResponse.StatusCode);

        }

        [Fact]
        public async Task Should_Create_User_With_Token()
        {
            var client = _factory.CreateClient();
            
            User user = new User();
            user.Username = "User" + this.RandomString(5,true);
            user.Emailaddress = "Test" + this.RandomString(6,true) + "@yahoo.com";
            user.Firstname = "Tom";
            user.Lastname = "TokenTest";
            user.Password = "Password@" + this.RandomString(9,true);
            user.Phonenumber = "412-567-8900";
            
            string JSON = JsonConvert.SerializeObject(user);
            var httpContent = new StringContent(JSON, Encoding.UTF8, "application/json");
            var createLoginResponse = await client.PostAsync("/v1/create", httpContent);
            
            Assert.Equal(HttpStatusCode.OK,createLoginResponse.StatusCode);
            
            
            Login loginUser = new Login();
            loginUser.Username = user.Username;
            loginUser.Password = user.Password;

            string loginJSON = JsonConvert.SerializeObject(loginUser);
            
            var loginHttpContent = new StringContent(JSON, Encoding.UTF8, "application/json");
            var loginResponse = await client.PostAsync("/v1/login", httpContent);
            
            Assert.Equal(HttpStatusCode.OK,loginResponse.StatusCode);
            
        }

        [Fact]
        public async Task Should_Check_Users_Token()
        {
            
            // CREATE USER
            var client = _factory.CreateClient();
            
            User user = new User();
            user.Username = "User" + this.RandomString(5,true);
            user.Emailaddress = "Test" + this.RandomString(6,true) + "@yahoo.com";
            user.Firstname = "Tom";
            user.Lastname = "TokenTest";
            user.Password = "Password@" + this.RandomString(9,true);
            user.Phonenumber = "412-567-8900";
            
            string JSON = JsonConvert.SerializeObject(user);
            var httpContent = new StringContent(JSON, Encoding.UTF8, "application/json");
            var createLoginResponse = await client.PostAsync("/v1/create", httpContent);
            
            Assert.Equal(HttpStatusCode.OK,createLoginResponse.StatusCode);
            
            // LOGIN USER
            Login loginUser = new Login();
            loginUser.Username = user.Username;
            loginUser.Password = user.Password;

            string loginJSON = JsonConvert.SerializeObject(loginUser);
            
            var loginHttpContent = new StringContent(JSON, Encoding.UTF8, "application/json");
            var loginResponse = await client.PostAsync("/v1/login", httpContent);
            
            Assert.Equal(HttpStatusCode.OK,loginResponse.StatusCode);
            
            //TODO: Need logout function
            
            // LOGIN A SECOND TIME
            Login loginUser1 = new Login();
            loginUser1.Username = user.Username;
            loginUser1.Password = user.Password;

            string login1JSON = JsonConvert.SerializeObject(loginUser);
            
            var login1HttpContent = new StringContent(JSON, Encoding.UTF8, "application/json");
            var login1Response = await client.PostAsync("/v1/login", httpContent);
            
            
            
        }



    }
}