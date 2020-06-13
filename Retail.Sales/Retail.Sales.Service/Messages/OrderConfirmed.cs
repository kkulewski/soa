namespace Retail.Sales.Service.Messages
{
    using NServiceBus;

    public class OrderConfirmed : IEvent
    {
        public string OrderId { get; set; }
    }
}
