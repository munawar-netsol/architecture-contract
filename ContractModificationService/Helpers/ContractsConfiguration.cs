using Contracts;
using MassTransit;
using Publisher.Events.Consumers;
using RabbitMQ.Client;

namespace Publisher.Helpers
{
    public static class ContractsConfiguration
    {
        public static void ConfigurePublisherContracts(this IRabbitMqBusFactoryConfigurator cfg)
        {
            cfg.Send<ContractSubmitMessageEnvelop>(x =>
            {
                x.UseRoutingKeyFormatter(context => context.Message.CustomerType);
                x.UseCorrelationId(context => context.TransactionId);
            });
        }

        public static void ConfgurePublisherContract<T>(this IRabbitMqBusFactoryConfigurator cfg, string entityName, string exchangeType) where T : MessageEnvelop
        {
            cfg.Message<T>(x =>
            {
                x.SetEntityName(entityName);
            });
            cfg.Publish<T>(x => x.ExchangeType = exchangeType);
        }

        public static void ConfigureSubscriberContract<T>(this IRabbitMqReceiveEndpointConfigurator cfg, IBusRegistrationContext cxt, string endPointName, string eventName, string routingKey, string exchangeType) where T : class, IConsumer
        {
            cfg.ConfigureConsumer<T>(cxt);

            cfg.Bind(eventName, s =>
            {
                s.RoutingKey = routingKey;
                s.ExchangeType = exchangeType;
            });
        }
    }
}
