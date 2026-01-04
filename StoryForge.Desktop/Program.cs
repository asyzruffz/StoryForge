using Microsoft.Extensions.DependencyInjection;
using MudBlazor.Services;
using Photino.Blazor;
using StoryForge.Desktop.UI;

namespace StoryForge.Desktop;

internal class Program
{
    [STAThread]
    static void Main(string[] args)
    {
        var builder = PhotinoBlazorAppBuilder.CreateDefault(args);

        builder.Services
            .AddLogging()
            .AddMudServices();

        builder.RootComponents.Add<App>("app");

        var app = builder.Build();

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
