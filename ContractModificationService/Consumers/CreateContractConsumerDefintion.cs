using MassTransit;
namespace Publisher.Events.Consumers
{

    public class CreateContractConsumerDefinition : ConsumerDefinition<CreateContractConsumer>
    {
        protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator, IConsumerConfigurator<CreateContractConsumer> consumerConfigurator)
        {
            endpointConfigurator.UseMessageRetry(r => r.Intervals(500, 1000));
        }
    }
}
