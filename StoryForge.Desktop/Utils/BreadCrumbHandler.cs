namespace StoryForge.Desktop.Utils;

internal class BreadCrumbHandler
{
    public record Item(string Label, string? Path, string? Icon);

    public Action? NavigationChange;

    public List<Item> Items { get; } = [];
}
