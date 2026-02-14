namespace StoryForge.Core.Data;

public class Project
{
    public ProjectId Id { get; set; } = ProjectId.Empty!;
    public Book Book { get; set; } = default!;
    public Author Author { get; set; } = default!;
}

public readonly record struct ProjectId(string Value) : ITypedId
{
    public static ProjectId Empty => new(TypedId.Empty);
    public static ProjectId New() => new(TypedId.New());
    public static ProjectId From(string value) => new(TypedId.From(value));
}
