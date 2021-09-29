namespace Retail.Events
{
    public interface IOrderConfirmed
    {
        public string OrderId { get; set; }
    }
}
