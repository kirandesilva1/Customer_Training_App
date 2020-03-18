using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace IO.Swagger.Models
{
    public class OrderItem
    {
        
        /// <summary>
        /// Gets or Sets Item Name
        /// </summary>
        [Required]
        [DataMember(Name="itemname")]
        public string ItemName { get; set; }
        
        /// <summary>
        /// Gets or Sets Qty
        /// </summary>
        [Required]
        [DataMember(Name="qty")]
        public int Qty { get; set; }
        
        /// <summary>
        /// Gets or Sets Unit Price
        /// </summary>
        [Required]
        [DataMember(Name="unitprice")]
        public float UnitPrice { get; set; }
        
        /// <summary>
        /// Gets or Sets Cost
        /// </summary>
        [Required]
        [DataMember(Name="cost")]
        public float Cost { get; set; }
        
        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        
        /// <summary>
        /// Returns true if objects are equal
        /// </summary>
        /// <param name="obj">Object to be compared</param>
        /// <returns>Boolean</returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((Order)obj);
        }
        
        /// <summary>
        /// Returns true if OrderItem instances are equal
        /// </summary>
        /// <param name="other">Instance of OrderItem to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(OrderItem other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return 
                (
                    ItemName == other.ItemName ||
                    ItemName != null &&
                    ItemName.Equals(other.ItemName)
                ) && 
                (
                    Qty == other.Qty ||
                    Qty.Equals(other.Qty)
                ) &&
                (
                    UnitPrice.Equals(other.UnitPrice)
                ) &&
                (
                    Cost.Equals(other.Cost)
                );
        }
        
    }
}