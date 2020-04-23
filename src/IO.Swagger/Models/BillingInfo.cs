using System.Runtime.Serialization;

namespace IO.Swagger.Models
{
    public class BillingInfo : IBillingInfo
    {
        [DataMember(Name="billinginfoId")]
        public string BillinginfoId { get; set; }

        [DataMember(Name="creditcardnumber")]
        public string CreditCardNumber { get; set; }
        
    }
}