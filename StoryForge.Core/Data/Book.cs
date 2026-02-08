namespace StoryForge.Core.Data;

public class Book
{
    public string Title { get; set; } = "Untitled";
    public string? Subtitle { get; set; }
    public string? Series { get; set; }
    public string? Volume { get; set; }
    public string? Genre { get; set; }
    public BookSummary Extra { get; set; } = default!;

    public static Book Invalid => new Book { Title = "Error", Extra = BookSummary.Invalid };
}
