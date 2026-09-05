using StoryForge.Core.Utils;

namespace StoryForge.Core.Data;

public class Summary
{
    public SummaryId Id { get; set; } = SummaryId.Empty;
    public string Sentence { get; set; } = string.Empty;
    public string Paragraph { get; set; } = string.Empty;
    public string Page { get; set; } = string.Empty;

    public static Summary New() => new() { Id = SummaryId.New() };
}

public readonly record struct SummaryId(string Value) : ITypedId
{
    public static SummaryId Empty => new(TypedId.Empty);
    public static SummaryId New() => new(TypedId.New());
    public static SummaryId From(string value) => new(TypedId.From(value));
}
