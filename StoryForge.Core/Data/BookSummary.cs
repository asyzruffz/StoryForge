namespace StoryForge.Core.Data;

public class BookSummary
{
    public string Situation { get; set; } = string.Empty;
    public SummaryId SummaryId { get; set; } = SummaryId.Empty;
    public Summary Summary { get; set; } = default!;

    public static BookSummary New()
    {
        var summary = Summary.New();
        return new() { SummaryId = summary.Id, Summary = summary };
    }

    public static BookSummary Invalid => new BookSummary { Summary = new Summary { Id = SummaryId.Empty } };
}
