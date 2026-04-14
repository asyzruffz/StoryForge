namespace StoryForge.Core.Misc;

public interface IFileDialogService
{
    Task<string?> ShowOpenFileAsync(string title, (string Name, string Extension)? filter = null);
    Task<string?> ShowSaveFileAsync(string title, (string Name, string Extension)? filter = null);
}
