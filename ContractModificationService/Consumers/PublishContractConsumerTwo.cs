using MassTransit;
using Contracts;
using Publisher.Events.Business;
using Publisher.Helpers;

namespace Publisher.Events.Consumers
{

    public class PublishContractConsumerTwo : BaseConsumer<ContractSubmitMessageEnvelop>
    {
        readonly ILogger<PublishContractConsumerTwo> _logger;
        public PublishContractConsumerTwo(ILogger<PublishContractConsumerTwo> logger)
        {
            _logger = logger;
        }
        public async override Task Consume(ConsumeContext<ContractSubmitMessageEnvelop> context)
        {
            await base.Consume(context);
            _logger.LogInformation("Received Text 1111111111111111111111111111111: {Text}", "GHIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIII");
            //await new PublishContractBusiness(_logger).PublishContract(context.Message.Value);

            //var contractNumber = context.Message.Value.ContractNumber;
            

        }
    }
}