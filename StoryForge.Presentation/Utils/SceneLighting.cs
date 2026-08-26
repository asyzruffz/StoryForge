using Microsoft.JSInterop;

namespace StoryForge.Presentation.Utils;

internal class SceneLighting : IAsyncDisposable
{
    private readonly Lazy<Task<IJSObjectReference>> moduleTask;

    public SceneLighting(IJSRuntime jsRuntime)
    {
        moduleTask = new(() => jsRuntime
            .InvokeAsync<IJSObjectReference>(
                "import", "./_content/StoryForge.Presentation/js/lighting.js")
            .AsTask());
    }

    public async ValueTask ToggleDarkMode()
    {
        var module = await moduleTask.Value;
        await module.InvokeVoidAsync("toggleDarkMode");
    }

    public async ValueTask<bool> IsDarkMode()
    {
        var module = await moduleTask.Value;
        return await module.InvokeAsync<bool>("isDarkModeEnabled");
    }

    public async ValueTask DisposeAsync()
    {
        if (moduleTask.IsValueCreated)
        {
            var module = await moduleTask.Value;
            await module.DisposeAsync();
        }
    }
}
