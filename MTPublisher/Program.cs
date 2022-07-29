using MassTransit;
using Serilog;

using MTPublisher.Workers;

Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(
        (hostContext, services) =>
        {
            services.AddMassTransit(x =>
            {
                x.SetKebabCaseEndpointNameFormatter();

                x.UsingRabbitMq(
                    (context, cfg) =>
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

                        cfg.ConfigureEndpoints(context);
                    }
                );
            });

            services.AddHostedService<Worker>();
        }
    )
    .Build();

await host.RunAsync();
