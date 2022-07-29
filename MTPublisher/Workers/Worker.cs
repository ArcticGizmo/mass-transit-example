using MassTransit;
using Serilog;

using MTCore.Contracts;

namespace MTPublisher.Workers;

public class Worker : BackgroundService
{
    private readonly IBus _bus;

    public Worker(IBus bus)
    {
        _bus = bus;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var now = DateTime.Now;
            var msg = new Message() { Name = $"Hello ${now.ToString()}" };
            await _bus.Publish(msg);
            Log.Information($"[Published] {msg.Name}");

            await Task.Delay(10_000, stoppingToken);
        }
    }
}
