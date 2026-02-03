namespace StoryForge.Core.Data;

public class Author
{
    public string Name { get; set; } = "Unnamed Author";
    public string? Email { get; set; }

    public static Author Invalid => new Author { Name = "Error" };
}
