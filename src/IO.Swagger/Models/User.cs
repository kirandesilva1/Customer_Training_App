using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Entities;

namespace IO.Swagger.Models
{
    public partial class User : EntityBase, IUser
    {
        /// <summary>
        /// Gets or Sets Id
        /// </summary>
        [DataMember(Name="userId")]
        public string UserId { get; set; }
        
        /// <summary>
        /// Gets or Sets User Name
        /// </summary>
        [Required]
        [DataMember(Name="username")]
        public string Username { get; set; }
        
        [Required]
        [DataMember(Name="firstname")]
        public string Firstname { get; set; }
        
        [Required]
        [DataMember(Name="lastname")]
        public string Lastname { get; set; }
        
        [Required]
        [DataMember(Name="emailaddress")]
        public string Emailaddress { get; set; }
        
        [Required]
        [DataMember(Name="phonenumber")]
        public string Phonenumber { get; set; }
        
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