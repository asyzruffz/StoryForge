using BlazorBlueprint.Components;
using Microsoft.Extensions.DependencyInjection;
using StoryForge.Presentation.Utils;

namespace StoryForge.Presentation;

public static class Extensions
{
    public static IServiceCollection AddPresentation(this IServiceCollection services) => services
        .AddBlazorBlueprintComponents()
        .AddJsUtils()
        .AddSingleton<BreadCrumbHandler>();

    static IServiceCollection AddJsUtils(this IServiceCollection services) => services
        .AddTransient<SceneLighting>();
}
