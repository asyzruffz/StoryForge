using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StoryForge.Application.Abstractions;
using StoryForge.Application.Services;
using StoryForge.Core.Services;
using StoryForge.Infrastructure.Database;
using StoryForge.Infrastructure.OpenAI;

namespace StoryForge.Simulator;

public static class StoryForgeInfrastructure
{
    public static IServiceCollection AddStoryForgeInfrastructure(this IServiceCollection services, IConfiguration config) => services
        .AddDatabase()
        .AddOpenAI(config);

    private static IServiceCollection AddDatabase(this IServiceCollection services) => services
        .AddSingleton<ApplicationDbContext>()
        .AddSingleton<IDataSession, DataSession>()
        .AddSingleton<IDataSessionFactory, DataSessionFactory>()
        .AddSingleton<ITemporaryStorage, TemporaryStorage>();

    private static IServiceCollection AddOpenAI(this IServiceCollection services, IConfiguration config) => services
        .Configure<OpenAIConfig>(config.GetSection("OpenAI"))
        .AddScoped<IOpenAIClient, OpenAIClient>();
}
