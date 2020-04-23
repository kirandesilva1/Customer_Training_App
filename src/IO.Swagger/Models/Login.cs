using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace IO.Swagger.Models
{
    public class Login : ILogin
    {
        [Required]
        [DataMember(Name="username")]
        public string Username { get; set; }
        
        [Required]
        [DataMember(Name="password")]
        public string Password { get; set; }
    }
}