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
                // x.AddConsumer<Consumer>();

                x.AddConsumers(entryAssembly);
                x.AddSagaStateMachines(entryAssembly);
                x.AddSagas(entryAssembly);
                x.AddActivities(entryAssembly);

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
