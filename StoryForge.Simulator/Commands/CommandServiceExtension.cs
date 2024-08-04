using Microsoft.Extensions.DependencyInjection;
using StoryForge.Simulator.Utils.Commands;

namespace StoryForge.Simulator.Commands;

public static class CommandServiceExtension
{
    public static IServiceCollection AddCommandService(this IServiceCollection services)
    {
        services
            .AddSingleton<VersionCommand>()
            .AddSingleton<ClearCommand>()
            .AddSingleton<PromptCommand>()
            .AddSingleton<TestCommand>();
        services.AddSingleton<CommandProcessor>();
        return services;
    }
}
