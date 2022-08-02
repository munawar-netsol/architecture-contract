using MassTransit;

namespace Publisher.Helpers
{
    public class BaseConsumer<T> : IConsumer<T> where T : MessageEnvelop
    {
        public async virtual Task Consume(ConsumeContext<T> context)
        {
            
        }
    }
}
