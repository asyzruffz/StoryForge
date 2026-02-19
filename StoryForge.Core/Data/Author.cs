using StoryForge.Core.Utils;

namespace StoryForge.Core.Data;

public class Author
{
    public AuthorId Id { get; set; } = AuthorId.Empty;
    public string Name { get; set; } = "Unnamed Author";
    public string? Email { get; set; }

    public static Author Invalid => new Author { Name = "Error" };
}

public readonly record struct AuthorId(string Value) : ITypedId
{
    public static AuthorId Empty => new(TypedId.Empty);
    public static AuthorId New() => new(TypedId.New());
    public static AuthorId From(string value) => new(TypedId.From(value));
}
