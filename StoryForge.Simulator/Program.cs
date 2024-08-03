
var host = Host.CreateDefaultBuilder(args)
    .ConfigureLogging(logging => logging.ClearProviders().AddProvider(NullLoggerProvider.Instance))
    .ConfigureServices(services =>
    {
        services.AddSingleton<IDataSession, DataSession>();
        services.AddSingleton<IDataSessionFactory, DataSessionFactory>();

        services.AddTomeSystem();
        services.AddCommandService();
        services.AddHostedService<TomeSimulator>();
    })
    .Build();

host.Run();