namespace Retail.Shipping.Host.Messages
{
    using NServiceBus;

    public class OrderShipped : IEvent
    {
        public string OrderId { get; set; }
    }
}
