using System.Diagnostics;
using MassTransit;
using MTCore.Contracts;
using Serilog;

namespace MTConsumer.Consumers;

public class MyConsumer : IConsumer<Message>
{
    private DateTime _epochStart = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

    public Task Consume(ConsumeContext<Message> ctx)
    {
        Log.Information($"[MyConsumer] {ctx.Message.Name, 5} -- Send @ {DateTimeToMS(ctx.SentTime)}");

        return Task.CompletedTask;
    }

    private double DateTimeToMS(DateTime? dt)
    {
        if (dt == null)
        {
            return 0f;
        }
        return ((DateTime)dt).ToUniversalTime().Subtract(_epochStart).TotalMilliseconds;
    }
}
