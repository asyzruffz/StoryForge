using StoryForge.Core.Utils;

namespace StoryForge.Core.Data;

public class Book
{
    public BookId Id { get; set; } = BookId.Empty;
    public string Title { get; set; } = "Untitled";
    public string? Subtitle { get; set; }
    public string? Series { get; set; }
    public string? Volume { get; set; }
    public string? Genre { get; set; }
    public BookSummary Extra { get; set; } = default!;

    public static Book Invalid => new Book { Title = "Error", Extra = BookSummary.Invalid };
}

public readonly record struct BookId(string Value) : ITypedId
{
    public static BookId Empty => new(TypedId.Empty);
    public static BookId New() => new(TypedId.New());
    public static BookId From(string value) => new(TypedId.From(value));
}
