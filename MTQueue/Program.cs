using System.Reflection;

using MassTransit;
using Serilog;

Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(
        (hostContext, services) =>
        {
            services.AddMassTransit(x =>
            {
                x.SetKebabCaseEndpointNameFormatter();

                // By default, sagas are in-memory, but should be changed to a durable
                // saga repository.
                x.SetInMemorySagaRepositoryProvider();

                var entryAssembly = Assembly.GetEntryAssembly();

                x.AddConsumers(entryAssembly);
                x.AddSagaStateMachines(entryAssembly);
                x.AddSagas(entryAssembly);
                x.AddActivities(entryAssembly);

                x.UsingInMemory(
                    (context, cfg) =>
                    {
                        cfg.ConfigureEndpoints(context);
                    }
                );
            });
        }
    )
    .Build();

await host.RunAsync();
