using System.Threading.Tasks;
using NServiceBus.MessageMutator;

namespace Retail.Frontend.Web.Mutators
{
    public class CommonNamespaceMutator : IMutateOutgoingTransportMessages
    {
        public Task MutateOutgoing(MutateOutgoingTransportMessageContext context)
        {
            var headers = context.OutgoingHeaders;
            headers["NServiceBus.EnclosedMessageTypes"] = $"Retail.{context.OutgoingMessage.GetType().Name}";
            return Task.CompletedTask;
        }
    }
}
