using Microsoft.Extensions.DependencyInjection;
using StoryForge.Desktop.Utils;

namespace StoryForge.Desktop;

internal static class Extensions
{
    public static IServiceCollection AddUIUtils(this IServiceCollection services) => services
        .AddSingleton<BreadCrumbHandler>();
}
