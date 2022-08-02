using MassTransit;
namespace Publisher.Events.Consumers
{

    public class PublishContractConsumerDefinition : ConsumerDefinition<PublishContractConsumer>
    {
        protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator, IConsumerConfigurator<PublishContractConsumer> consumerConfigurator)
        {
            endpointConfigurator.UseMessageRetry(r => r.Intervals(500, 1000));
        }
    }
}
