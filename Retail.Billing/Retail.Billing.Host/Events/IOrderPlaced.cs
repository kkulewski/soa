namespace Retail.Events
{
    public interface IOrderPlaced
    {
        public string OrderId { get; set; }
        public string CustomerId { get; set; }
    }
}
