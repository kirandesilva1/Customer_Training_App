using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Entities;

namespace IO.Swagger.Models
{
    public partial class User : EntityBase
    {
        /// <summary>
        /// Gets or Sets User Name
        /// </summary>
        [Required]
        [DataMember(Name="username")]
        public string Username { get; set; }
        
        /// <summary>
        /// Gets or Sets Password
        /// </summary>
        [Required]
        [DataMember(Name="password")]
        public string Password { get; set; }

        [DataMember(Name = "token")] 
        public string Token { get; set; }
    }
}