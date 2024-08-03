using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using StoryForge.Core.Services;
using StoryForge.Core.Services.Implementations;

namespace StoryForge.Simulator;

public static class StoryForgeExtension
{
    public static IHostBuilder SetNullLogger(this IHostBuilder builder) => builder
        .ConfigureLogging(logging => logging
            .ClearProviders()
            .AddProvider(NullLoggerProvider.Instance));

    public static IServiceCollection AddStoryForgeSystem(this IServiceCollection services) => services
        .AddSingleton<IRandomService, RandomService>();
    //    .AddSingleton<INameGenerator, NameGenerator>()
    //    .AddSingleton<ICharacterGenerator, CharacterGenerator>()
    //    .AddSingleton<IDefaultExistence, DefaultExistence>()
    //    .AddSingleton<TomeSystem>();
}
