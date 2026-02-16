namespace StoryForge.Core.Data;

public class Project
{
    public string FilePath { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public Book Book { get; set; } = default!;
    public Author Author { get; set; } = default!;
}
