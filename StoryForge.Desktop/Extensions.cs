using Microsoft.Extensions.DependencyInjection;
using StoryForge.Application.Services;
using StoryForge.Core.Services;
using StoryForge.Desktop.Utils;

namespace StoryForge.Desktop;

internal static class Extensions
{
    public static IServiceCollection AddUIUtils(this IServiceCollection services) => services
        .AddSingleton<BreadCrumbHandler>();

    public static IServiceCollection AddApplication(this IServiceCollection services) => services
        .AddMediatR(config => config
            .RegisterServicesFromAssembly(Application.AssemblyReference.Assembly))
        .AddStoryForgeSystem();

    private static IServiceCollection AddStoryForgeSystem(this IServiceCollection services) => services
        .AddSingleton<IRandomService, RandomService>()
        .AddScoped<IAIService, AIService>();
}
