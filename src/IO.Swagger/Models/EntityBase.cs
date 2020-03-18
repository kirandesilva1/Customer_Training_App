using System;
using System.Runtime.Serialization;

namespace Entities
{
    public class EntityBase
    {
       // public Int64 Id { get; protected set; }
       [DataMember(Name="Id")]
       public Guid Id { get; set; }
    }
}