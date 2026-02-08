namespace StoryForge.Core.Data;

public class BookSummary
{
    public string Situation { get; set; } = string.Empty;
    public Summary Summary { get; set; } = default!;

    public static BookSummary New() => new() { Summary = Summary.New() };
    public static BookSummary Invalid => new BookSummary { Summary = new Summary { Id = SummaryId.Empty } };
}
