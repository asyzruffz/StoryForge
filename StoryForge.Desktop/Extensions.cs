using Microsoft.Extensions.DependencyInjection;
using MudBlazor.Services;
using Photino.Blazor;
using StoryForge.Application.Services;
using StoryForge.Core.Services;
using StoryForge.Desktop.UI;
using StoryForge.Desktop.Utils;
using StoryForge.Infrastructure.Database;
using StoryForge.Infrastructure.Database.SQLite;

namespace StoryForge.Desktop;

internal static class Extensions
{
    public static IServiceCollection AddPresentation(this IServiceCollection services) => services
        .AddSingleton<BreadCrumbHandler>()
        .AddMudServices();

    public static IServiceCollection AddApplication(this IServiceCollection services) => services
        .AddMediatR(config => config
            .RegisterServicesFromAssembly(Application.AssemblyReference.Assembly))
        .AddStoryForgeSystem();

    public static IServiceCollection AddInfrastructure(this IServiceCollection services) => services
        .AddDatabase();

    public static void AddAppComponent(this PhotinoBlazorAppBuilder builder) => builder
        .RootComponents.Add<App>("app");

    public static IServiceProvider SetupInfrastructure(this IServiceProvider services) => services
        .InitDatabase();

    private static IServiceCollection AddStoryForgeSystem(this IServiceCollection services) => services
        .AddSingleton<IProjectSessionHandler, ProjectSessionHandler>()
        .AddScoped<IProjectFileStorage, ProjectFileStorage>()
        .AddSingleton<IRandomService, RandomService>()
        .AddScoped<IAIService, AIService>();

    private static IServiceCollection AddDatabase(this IServiceCollection services) => services
        .AddSingleton<ApplicationDbContext>()
        .AddSingleton<IApplicationDataSession, ApplicationDataSession>()
        .AddScoped<IProjectScopeContext, ProjectScopeContext>()
        .AddScoped<ProjectDbFactory>()
        .AddScoped(provider =>
        {
            var ctx = provider.GetRequiredService<IProjectScopeContext>();
            var factory = provider.GetRequiredService<ProjectDbFactory>();
            return factory.CreateDbContext(ctx.ProjectFilePath);
        })
        .AddScoped<IDataSession, DataSession>()
        .AddSingleton<ITemporaryStorage, TemporaryStorage>();

    private static IServiceProvider InitDatabase(this IServiceProvider services)
    {
        services.GetRequiredService<IApplicationDataSession>()
            .EnsureCreated();
        return services;
    }
}
