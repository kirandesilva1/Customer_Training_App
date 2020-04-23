namespace IO.Swagger.Models
{
    public interface IOrderItem
    {
        string ItemName { get; set; }
        int Qty { get; set; }
        float UnitPrice { get; set; }
        float Cost { get; set; }
        
    }
}