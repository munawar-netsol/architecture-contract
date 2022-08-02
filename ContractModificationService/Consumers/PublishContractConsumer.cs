using MassTransit;
using Contracts;
using Publisher.Events.Business;
using Publisher.Helpers;

namespace Publisher.Events.Consumers
{
    public class PublishContractConsumer : BaseConsumer<ContractSubmitMessageEnvelop>
    {

        readonly ILogger<PublishContractConsumer> _logger;
        public PublishContractConsumer(ILogger<PublishContractConsumer> logger)
        {
            _logger = logger;
        }
        public async override Task Consume(ConsumeContext<ContractSubmitMessageEnvelop> context)
        {
            _logger.LogInformation("Received Text 1111111111111111111111111111111: {Text}", "GHIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIII");
            await base.Consume(context);

            //var contractNumber = context.Message.Value.ContractNumber;
            //_logger.LogInformation("Received Text: {Text}", contractNumber);

            //await new PublishContractBusiness(_logger).PublishContract(context.Message.Value);
        }
    }
}