using Contracts;
using MassTransit;
using Newtonsoft.Json;
using Publisher.Models;

namespace Publisher.Events.Publishers
{
    public class SubmitContractEvents
    {
        public async static Task SubmitContract(IBus bus)
        {
            var customerType = "PRIORITY";
            var cont = new Contract()
            {
                ContractId = 1,
                ContractNumber = "ABC-1",
                ContractAssets = new List<ContractAsset>
                {
                    new ContractAsset() { VinNumber = "KHI-1234" }
                }
            };

            await bus.Publish(new ContractCreateMessageEnvelop
            {
                CustomerType = customerType,
                Value = new Contract()
                {
                    ContractId = 123,
                    ContractNumber = "ABC-123",
                    ContractAssets = new List<ContractAsset> {
                        new ContractAsset() { VinNumber = "VIN-1234" }
                    }
                }
            });
            
            cont.ContractAssets.First().VinNumber = "LHR-5678";

            //customerType = "REGULAR";
            //await bus.Publish(new ContractSubmitMessageEnvelop
            //{
            //    CustomerType = customerType,
            //    Value = new ContractModification()
            //});
        }
    }
}
