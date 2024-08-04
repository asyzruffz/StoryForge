namespace StoryForge.Core.Data;

public record Title(string Content)
{
    public static Title Empty => new(string.Empty);
}
