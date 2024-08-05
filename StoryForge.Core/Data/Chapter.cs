namespace StoryForge.Core.Data;

public class Chapter
{
    public ChapterId Id { get; set; } = ChapterId.Empty;
    public Chapter? Prev { get; set; }
    public Chapter? Next { get; set; }
    public Title Title { get; set; } = Title.Empty;
    public IContentSection Content { get; set; } = new EmptySection();
}

public readonly record struct ChapterId(string Value) : ITypedId
{
    public static ChapterId Empty => new(TypedId.Empty);
    public static ChapterId New() => new(TypedId.New());
    public static ChapterId From(string value) => new(TypedId.From(value));
}
