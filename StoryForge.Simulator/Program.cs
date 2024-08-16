using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StoryForge.Simulator;
using StoryForge.Simulator.Commands;

var host = Host.CreateDefaultBuilder(args)
    .SetNullLogger()
    .ConfigureServices((context, services) =>
    {
        services.AddStoryForgeInfrastructure(context.Configuration);
        services.AddStoryForgeApplication();

        services.AddCommandService();
        services.AddHostedService<StoryForgeSimulator>();
    })
    .Build();

host.Run();
