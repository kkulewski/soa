namespace Retail.Sales.Service.Messages
{
    using NServiceBus;

    public class OrderShipped : IEvent
    {
        public string OrderId { get; set; }
    }
}
