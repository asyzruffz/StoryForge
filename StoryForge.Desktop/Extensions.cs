using Microsoft.Extensions.DependencyInjection;
using MudBlazor;
using MudBlazor.Services;
using Photino.Blazor;
using StoryForge.Application.Projects;
using StoryForge.Core.Projects;
using StoryForge.Core.Storage;
using StoryForge.Desktop.UI;
using StoryForge.Desktop.Utils;
using StoryForge.Infrastructure.Database;
using StoryForge.Infrastructure.Database.SQLite;

namespace StoryForge.Desktop;

internal static class Extensions
{
    public static IServiceCollection AddPresentation(this IServiceCollection services) => services
        .AddSingleton<BreadCrumbHandler>()
        .AddMudServices(config => 
        {
            config.SnackbarConfiguration.SnackbarVariant = Variant.Outlined;
        });

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
        .AddScoped<IProjectFileStorage, ProjectFileStorage>();

    private static IServiceCollection AddDatabase(this IServiceCollection services) => services
        .AddSingleton<ApplicationDbContext>()
        .AddSingleton<IApplicationDataSession, ApplicationDataSession>()
        .AddScoped<ProjectDbFactory>()
        .AddScoped(provider => provider
            .GetRequiredService<ProjectDbFactory>()
            .CreateDbContext(provider))
        .AddScoped<IDataSession, DataSession>();

    private static IServiceProvider InitDatabase(this IServiceProvider services)
    {
        services.GetRequiredService<IApplicationDataSession>()
            .EnsureCreatedAsync(default)
            .Wait();
        return services;
    }
}
