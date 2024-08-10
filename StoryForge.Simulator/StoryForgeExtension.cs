using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using StoryForge.Application.Abstractions;
using StoryForge.Application.Services;
using StoryForge.Core.Services;
using StoryForge.Infrastructure.OpenAI;

namespace StoryForge.Simulator;

public static class StoryForgeExtension
{
    public static IHostBuilder SetNullLogger(this IHostBuilder builder) => builder
        .ConfigureLogging(logging => logging
            .ClearProviders()
            .AddProvider(NullLoggerProvider.Instance));

    public static IServiceCollection AddOpenAI(this IServiceCollection services, IConfiguration config) => services
        .Configure<OpenAIConfig>(config.GetSection("OpenAI"))
        .AddScoped<IOpenAIClient, OpenAIClient>();

    public static IServiceCollection AddStoryForgeSystem(this IServiceCollection services) => services
        .AddSingleton<IRandomService, RandomService>()
        .AddScoped<IAIService, AIService>();
    //    .AddSingleton<ICharacterGenerator, CharacterGenerator>()
    //    .AddSingleton<IDefaultExistence, DefaultExistence>()
    //    .AddSingleton<TomeSystem>();
}
