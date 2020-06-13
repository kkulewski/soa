namespace Retail.Billing.Host.Events
{
    public class OrderPlaced
    {
        public string OrderId { get; set; }
        public string CustomerId { get; set; }
    }
}
