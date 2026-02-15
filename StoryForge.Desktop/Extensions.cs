using Microsoft.Extensions.DependencyInjection;
using StoryForge.Application.Services;
using StoryForge.Core.Services;
using StoryForge.Desktop.Utils;
using StoryForge.Infrastructure.Database;
using StoryForge.Infrastructure.Database.SQLite;

namespace StoryForge.Desktop;

internal static class Extensions
{
    public static IServiceCollection AddUIUtils(this IServiceCollection services) => services
        .AddSingleton<BreadCrumbHandler>();

    public static IServiceCollection AddApplication(this IServiceCollection services) => services
        .AddMediatR(config => config
            .RegisterServicesFromAssembly(Application.AssemblyReference.Assembly))
        .AddStoryForgeSystem();

    public static IServiceCollection AddInfrastructure(this IServiceCollection services) => services
        .AddDatabase();

    private static IServiceCollection AddStoryForgeSystem(this IServiceCollection services) => services
        .AddSingleton<IRandomService, RandomService>()
        .AddScoped<IAIService, AIService>();

    private static IServiceCollection AddDatabase(this IServiceCollection services) => services
        .AddSingleton<ApplicationDbContext>()
        .AddSingleton<IApplicationDataSession, ApplicationDataSession>()
        .AddSingleton<IDataSession, DataSession>()
        .AddSingleton<IDataSessionFactory, DataSessionFactory>()
        .AddSingleton<ITemporaryStorage, TemporaryStorage>();
}
