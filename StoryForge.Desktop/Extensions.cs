using Keystone.Application;
using Microsoft.Extensions.DependencyInjection;
using Photino.Blazor;
using Photino.NET;
using StoryForge.Application.Projects;
using StoryForge.Core.Misc;
using StoryForge.Core.Projects;
using StoryForge.Core.Storage;
using StoryForge.Desktop.Services;
using StoryForge.Infrastructure.Database;
using StoryForge.Infrastructure.Database.SQLite;

namespace StoryForge.Desktop;

internal static class Extensions
{
    public static IServiceCollection AddWindowsService(this IServiceCollection services) => services
        .AddTransient<IFileDialogService, FileDialogService>();

    public static IServiceCollection AddApplication(this IServiceCollection services) => services
        .AddKeystoneApplication()
        .RegisterOperationHandlers(Application.AssemblyReference.Assembly)
        .AddStoryForgeSystem();

    public static IServiceCollection AddInfrastructure(this IServiceCollection services) => services
        .AddDatabase();

    public static void AddAppComponent(this PhotinoBlazorAppBuilder builder) => builder
        .RootComponents.Add<App>("app");

    public static PhotinoWindow SetupDefault(this PhotinoWindow window, string title)
    {
        window
            .SetLogVerbosity(0)
            .SetUseOsDefaultSize(true)
            .SetUseOsDefaultLocation(true)
            .SetFileSystemAccessEnabled(true)
            .SetTitle(title);

        /*window.WebMessageReceivedHandler += (sender, message) =>
        {
            Console.WriteLine($"Received message from web: {message}");
        };*/

        AppDomain.CurrentDomain.UnhandledException += (sender, error) =>
        {
            window.ShowMessage("Fatal exception", error.ExceptionObject.ToString());
        };

        return window;
    }

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
