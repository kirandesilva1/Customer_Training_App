using System;
using IO.Swagger.Attributes;
using IO.Swagger.BusinessLayer;
using IO.Swagger.Helpers;
using IO.Swagger.Models;
using IO.Swagger.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Annotations;

namespace IO.Swagger.Controllers
{
    [ApiController]
    public class LoginApiController: ControllerBase
    {
        private readonly ILoginService _loginService;
        private readonly ITokenService _tokenService;
        private readonly AppSettings _appSettings;
        
        private LoginManager loginManager; 
        
        public LoginApiController(IOptions<AppSettings> appSettings ,ILoginService loginService, ITokenService tokenService)
        {
            _loginService = loginService;
            _tokenService = tokenService;
            _appSettings = appSettings.Value;
            loginManager = new LoginManager(_appSettings,_loginService, _tokenService);
            
        }
        
        /// <summary>
        /// Authenticate User
        /// </summary>
        /// <remarks>This is used to authenticate user</remarks>
        /// <param name="body">Login credentials</param>
        /// <response code="0">successful operation</response>
        [HttpPost]
        [Route("/v1/login")]
        [ValidateModelState]
        [SwaggerOperation("Authenticate")]
        [SwaggerResponse(statusCode: 200, type: typeof(string), description: "successful operation")]
        public virtual IActionResult Authenticate([FromBody] Login body)
        {
            try
            {
                var user = loginManager.Authenticate(body.Username, body.Password);
                if (user != null)
                {
                    return Ok(user);
                }
                else
                {
                    throw new Exception();
                }
                    
            }
            catch (Exception ex)
            {
                return BadRequest("Invalid username or password ");
            }
        }
        
        
        /// <summary>
        /// Create User
        /// </summary>
        /// <remarks>This is used to create a user</remarks>
        /// <param name="body"></param>
        /// <response code="0">successful operation</response>
        [HttpPost]
        [Route("/v1/create")]
        [ValidateModelState]
        [SwaggerOperation("Create")]
        [SwaggerResponse(statusCode: 200, type: typeof(string), description: "successful operation")]
        public virtual IActionResult Create([FromBody] User body)
        {
            try
            {
                loginManager.CreateLogin(body);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest("Invalid user");
            }
        }
    }
}