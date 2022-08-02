using MassTransit;
namespace Publisher.Events.Consumers
{

    public class PublishContractConsumerTwoDefinition : ConsumerDefinition<PublishContractConsumerTwo>
    {
        protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator, IConsumerConfigurator<PublishContractConsumerTwo> consumerConfigurator)
        {
            endpointConfigurator.UseMessageRetry(r => r.Intervals(500, 1000));
        }
    }
}
