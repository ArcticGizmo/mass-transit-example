using MTPublisher;

using Serilog;

Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();


IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();
