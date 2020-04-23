using System.Collections.Generic;

namespace IO.Swagger.Models
{
    public interface IOrder
    {
        string OrderId { get; set; }
        Customer Customer { get; set; }
        string Description { get; set; }
        Address Shipaddress { get; set; }
        List<OrderItem> OrderItems { get; set; }
        Status Status { get; set; }
        
    }
}