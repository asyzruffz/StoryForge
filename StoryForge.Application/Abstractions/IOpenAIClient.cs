using StoryForge.Core.Utils;

namespace StoryForge.Application.Abstractions;

public interface IOpenAIClient
{
    Task<Result<IEnumerable<string>>> Complete(string message, CancellationToken cancellationToken);
    void Reset();
}
