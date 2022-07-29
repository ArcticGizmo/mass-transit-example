namespace Company.Consumers
{
    using MassTransit;

    public class MTConsumerConsumerDefinition :
        ConsumerDefinition<MTConsumerConsumer>
    {
        protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator, IConsumerConfigurator<MTConsumerConsumer> consumerConfigurator)
        {
            endpointConfigurator.UseMessageRetry(r => r.Intervals(500, 1000));
        }
    }
}