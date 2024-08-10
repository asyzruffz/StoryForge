namespace StoryForge.Core.Services;

public interface IAIService
{
    Task<string> Complete(string preface, CancellationToken cancellationToken);
}
