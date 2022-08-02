using MassTransit;
using Contracts;
using Publisher.Helpers;
using ContractModificationService.Models;

namespace Publisher.Events.Consumers
{

    public class CreateContractConsumer : BaseConsumer<ContractCreateMessageEnvelop>
    {

        readonly ILogger<CreateContractConsumer> _logger;
        public CreateContractConsumer(ILogger<CreateContractConsumer> logger)
        {
            _logger = logger;
        }
        public async override Task Consume(ConsumeContext<ContractCreateMessageEnvelop> context)
        {
            _logger.LogInformation("Received Text 1111111111111111111111111111111: {Text}", "GHIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIII");
            await base.Consume(context);

            var contractNumber = context.Message.Value.ContractNumber;
            _logger.LogInformation("Received Text: {Text}", contractNumber);

            //await new PublishContractBusiness(_logger).PublishContract(context.Message.Value);
            //await context.RespondAsync(new ContractSubmitMessageEnvelop
            //{
            //    Value = new ContractModification()
            //    {
            //        ContractId = 15889,
            //        ContractNumber = "ADSSASDD"
            //    }
            //});
        }
    }
}