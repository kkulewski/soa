namespace Retail.Billing.Host.Mutators
{
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;
    using NServiceBus.MessageMutator;

    public class CommonIncomingNamespaceMutator : IMutateIncomingTransportMessages
    {
        public Task MutateIncoming(MutateIncomingTransportMessageContext context)
        {
            var receivedType = context.Headers["NServiceBus.EnclosedMessageTypes"];

            var localType = Assembly
                .GetExecutingAssembly().DefinedTypes
                .First(t => t.Name == receivedType);

            context.Headers["NServiceBus.EnclosedMessageTypes"] = localType.AssemblyQualifiedName;
            return Task.CompletedTask;
        }
    }
}
