namespace Retail.Billing.Host.Messages
{
    using NServiceBus;

    public class OrderPaid : IEvent
    {
        public string OrderId { get; set; }
    }
}
