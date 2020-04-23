using System;
using System.Runtime.Serialization;
using System.Security.Claims;
using Entities;
using Microsoft.IdentityModel.Tokens;

namespace IO.Swagger.Models
{
    public class Token : EntityBase, IToken
    {
        /// <summary>
        /// Gets or Sets Secret Key
        /// </summary>
        [DataMember(Name = "secretkey")]
        public string SecretKey { get; set; } 

        /// <summary>
        /// Gets or Sets Security Algorithm
        /// </summary>
        [DataMember(Name = "securityalgorithm")]
        public string SecurityAlgorithm { get; set; } = SecurityAlgorithms.HmacSha256Signature;

        /// <summary>
        /// Gets or Sets ExpirationDate
        /// </summary>
        [DataMember(Name = "expireminutes")]
        public int ExpireMinutes { get; set; } = 10080; // 7 days
        
        /// <summary>
        /// Gets or Sets IssueDate
        /// </summary>
        [DataMember(Name="claims")]
        public Claim[] Claims { get; set; }
        
    }
}