using System.Reflection;
using MassTransit;
using Serilog;

using MTConsumer.Consumers;

Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(
        (hostContext, services) =>
        {
            services.AddMassTransit(x =>
            {
                x.SetKebabCaseEndpointNameFormatter();

                var entryAssembly = Assembly.GetEntryAssembly();
                x.AddConsumers(entryAssembly);
                // x.AddConsumer<MyConsumer>(); // -- the same in this project

                x.UsingRabbitMq(
                    (ctx, cfg) =>
                    {
                        cfg.Host(
                            "localhost",
                            "/",
                            h =>
                            {
                                h.Username("guest");
                                h.Password("guest");
                            }
                        );

                        cfg.ConfigureEndpoints(ctx);
                    }
                );
            });
        }
    )
    .Build();

await host.RunAsync();
