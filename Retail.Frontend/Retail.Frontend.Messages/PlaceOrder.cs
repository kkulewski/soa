namespace Retail.Frontend.Messages
{
    using NServiceBus;

    public class PlaceOrder : ICommand
    {
        public string OrderId { get; set; }
    }
}
