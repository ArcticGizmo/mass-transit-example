namespace Company.Consumers
{
    using System.Threading.Tasks;
    using MassTransit;
    using Contracts;

    public class MTConsumerConsumer :
        IConsumer<MTConsumer>
    {
        public Task Consume(ConsumeContext<MTConsumer> context)
        {
            return Task.CompletedTask;
        }
    }
}