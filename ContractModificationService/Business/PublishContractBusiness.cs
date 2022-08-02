using MassTransit;
using Contracts;
using ContractModificationService.Contracts;

namespace Publisher.Events.Business
{
    public class PublishContractBusiness
    {

        readonly ILogger _logger;
        public PublishContractBusiness(ILogger logger)
        {
            _logger = logger;
        }
        public async Task PublishContract(IContractModification cont)
        {
            var contractNumber = cont.ContractNumber;
            _logger.LogInformation("Received Text: {Text}", contractNumber);
        }
    }
}