namespace Retail.Sales.Service
{
    using NServiceBus;

    public class PlaceOrder : ICommand
    {
        public string OrderId { get; set; }
    }
}
