namespace Retail.Shipping.Host.Messages
{
    using NServiceBus;

    public class OrderConfirmed : IEvent
    {
        public string OrderId { get; set; }
    }
}
