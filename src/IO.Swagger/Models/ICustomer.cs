namespace IO.Swagger.Models
{
    public interface ICustomer
    {
        string CustomerId { get; set; }
        string Name { get; set; }
        int? Groupid { get; set; }
        int? Numberoforders { get; set; }
        Address Address { get; set; }
        BillingInfo Billing { get; set; }
        
    }
}