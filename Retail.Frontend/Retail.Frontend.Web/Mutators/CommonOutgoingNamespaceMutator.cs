namespace Retail.Frontend.Web.Mutators
{
    using System.Threading.Tasks;
    using NServiceBus.MessageMutator;

    public class CommonOutgoingNamespaceMutator : IMutateOutgoingTransportMessages
    {
        public Task MutateOutgoing(MutateOutgoingTransportMessageContext context)
        {
            var headers = context.OutgoingHeaders;
            headers["NServiceBus.EnclosedMessageTypes"] = $"{context.OutgoingMessage.GetType().Name}";
            return Task.CompletedTask;
        }
    }
}
