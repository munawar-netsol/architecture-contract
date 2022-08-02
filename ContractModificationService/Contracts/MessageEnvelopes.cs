using ContractModificationService.Contracts;
using Publisher.Helpers;

namespace Contracts
{    
    public record ContractSubmitMessageEnvelop : MessageEnvelop
    {
        public IContractModification Value { get; init; }
    }

    public record ContractCreateMessageEnvelop : MessageEnvelop
    {
        public IContract Value { get; init; }
    }
}