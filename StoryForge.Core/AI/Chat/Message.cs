namespace StoryForge.Core.AI.Chat;

public sealed record Message(string Id, MessageRole Role, string Content, IReadOnlyDictionary<string, string>? Metadata = null)
{
    public static Message CreateUser(string content) => new(Guid.NewGuid().ToString(), MessageRole.User, content);
}

public enum MessageRole
{
    System,
    User,
    Assistant,
    Tool
}
