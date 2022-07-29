using MassTransit;
using MTConsumer.Consumers;

namespace Company.Consumers;

public class MTConsumerConsumerDefinition : ConsumerDefinition<MyConsumer>
{
    protected override void ConfigureConsumer(
        IReceiveEndpointConfigurator endpointConfigurator,
        IConsumerConfigurator<MyConsumer> consumerConfigurator
    )
    {
        endpointConfigurator.UseMessageRetry(r => r.Intervals(500, 1000));
    }
}
