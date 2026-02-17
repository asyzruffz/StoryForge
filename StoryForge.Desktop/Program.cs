using Microsoft.Extensions.DependencyInjection;
using Photino.Blazor;

namespace StoryForge.Desktop;

internal class Program
{
    [STAThread]
    static void Main(string[] args)
    {
        var builder = PhotinoBlazorAppBuilder.CreateDefault(args);

        builder.Services
            .AddLogging()
            .AddApplication()
            .AddInfrastructure()
            .AddPresentation();

        builder.AddAppComponent();

        var app = builder.Build();

        app.Services
            .SetupInfrastructure();

        app.MainWindow
            .SetUseOsDefaultSize(true)
            .SetUseOsDefaultLocation(true)
            .SetIconFile("favicon.ico")
            .SetTitle("Story Forge");

        AppDomain.CurrentDomain.UnhandledException += (sender, error) =>
        {
            app.MainWindow.ShowMessage("Fatal exception", error.ExceptionObject.ToString());
        };

        app.Run();
    }
}
