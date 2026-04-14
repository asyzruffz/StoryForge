using Photino.Blazor;
using Photino.NET;
using StoryForge.Core.Misc;

namespace StoryForge.Desktop.Services;

internal class FileDialogService : IFileDialogService
{
    private readonly PhotinoWindow window;

    public FileDialogService(PhotinoBlazorApp app)
    {
        window = app.MainWindow;
    }

    public async Task<string?> ShowOpenFileAsync(string title, (string Name, string Extension)? filter = null)
    {
        // TODO: Save the last used directory and use it as the initial directory for the file dialog

        var filePaths = await window.ShowOpenFileAsync(title, filters: ProcessFilter(filter))
            .ConfigureAwait(false);
        return filePaths?.FirstOrDefault();
    }

    public async Task<string?> ShowSaveFileAsync(string title, (string Name, string Extension)? filter = null)
    {
        var filePath = await window.ShowSaveFileAsync(title, filters: ProcessFilter(filter))
            .ConfigureAwait(false);
        return filePath;
    }

    (string, string[])[]? ProcessFilter((string Name, string Extension)? filter)
    {
        if (filter == null) return null;
        return [(filter.Value.Name, [filter.Value.Extension])];
    }
}
