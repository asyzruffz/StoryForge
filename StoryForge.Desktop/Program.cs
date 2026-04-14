using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Photino.Blazor;
using StoryForge.MudPresentation;

namespace StoryForge.Desktop;

internal class Program
{
    [STAThread]
    static void Main(string[] args)
    {
        string windowTitle = "Story Forge";

        var builder = PhotinoBlazorAppBuilder.CreateDefault(args);

        builder.Services
            .AddLogging(config => config.AddConsole())
            .AddApplication()
            .AddInfrastructure()
            .AddPresentation()
            .AddWindowsService();

        builder.AddAppComponent();

        var app = builder.Build();

        app.Services
            .SetupInfrastructure();

        app.MainWindow.SetupDefault(windowTitle);

        //app.UseStatusCodePagesWithRedirects("/StatusCode/{0}");

        app.Run();
    }
}
