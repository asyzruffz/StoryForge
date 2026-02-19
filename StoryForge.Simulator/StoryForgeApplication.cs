using Microsoft.Extensions.DependencyInjection;
using StoryForge.Application.Misc;
using StoryForge.Application.Services;
using StoryForge.Core.Misc;
using StoryForge.Core.Services;

namespace StoryForge.Simulator;

public static class StoryForgeApplication
{
    public static IServiceCollection AddStoryForgeApplication(this IServiceCollection services) => services
        .AddMediatR(config => config
            .RegisterServicesFromAssembly(Application.AssemblyReference.Assembly))
        .AddStoryForgeSystem();

    private static IServiceCollection AddStoryForgeSystem(this IServiceCollection services) => services
        .AddSingleton<IRandomService, RandomService>();
}
