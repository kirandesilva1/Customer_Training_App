using System;
using System.Runtime.Serialization;
using Entities;
using Microsoft.IdentityModel.Tokens;

namespace IO.Swagger.Models
{
    public class Token : EntityBase
    {
        /// <summary>
        /// Gets or Sets ExpirationDate
        /// </summary>
        [DataMember(Name="expirationdate")]
        public DateTime? ExpirationDate { get; set; }
        
        /// <summary>
        /// Gets or Sets IssueDate
        /// </summary>
        [DataMember(Name="issuedate")]
        public DateTime? IssueDate { get; set; }
        
    }
}