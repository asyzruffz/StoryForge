namespace StoryForge.Core.AI.Services;

public interface IAIService
{
    Task<string> Complete(string preface, CancellationToken cancellationToken);
}
