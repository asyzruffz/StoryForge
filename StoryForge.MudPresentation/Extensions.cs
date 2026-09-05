using Microsoft.Extensions.DependencyInjection;
using MudBlazor;
using MudBlazor.Services;
using StoryForge.MudPresentation.Utils;

namespace StoryForge.MudPresentation;

public static class Extensions
{
    public static IServiceCollection AddPresentation(this IServiceCollection services) => services
        .AddSingleton<BreadCrumbHandler>()
        .AddMudServices(config => 
        {
            config.SnackbarConfiguration.SnackbarVariant = Variant.Outlined;
        });
}
