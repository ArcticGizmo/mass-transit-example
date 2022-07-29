using MassTransit;
using MTCore.Contracts;
using Serilog;

namespace MTConsumer.Consumers;

public class MyConsumer : IConsumer<Message>
{
    public Task Consume(ConsumeContext<Message> ctx)
    {
        Log.Information($"[MyConsumer] {ctx.Message.Name}");
        return Task.CompletedTask;
    }
}
