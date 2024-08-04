using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StoryForge.Application.Services;
using StoryForge.Core.Services;
using StoryForge.Infrastructure;
using StoryForge.Simulator;
using StoryForge.Simulator.Commands;

var host = Host.CreateDefaultBuilder(args)
    .SetNullLogger()
    .ConfigureServices(services =>
    {
        services.AddMediatR(config => config
            .RegisterServicesFromAssembly(StoryForge.Application.AssemblyReference.Assembly));

        services.AddSingleton<IDataSession, DataSession>();
        services.AddSingleton<IDataSessionFactory, DataSessionFactory>();
        services.AddSingleton<ITemporaryStorage, TemporaryStorage>();

        services.AddStoryForgeSystem();
        services.AddCommandService();
        services.AddHostedService<StoryForgeSimulator>();
    })
    .Build();

host.Run();