namespace StoryForge.Application.Abstractions;

public interface IOpenAIClient
{
    Task<IEnumerable<string>> Complete(string message, CancellationToken cancellationToken);
    void Reset();
}
