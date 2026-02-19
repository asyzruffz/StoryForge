using StoryForge.Core.AI.Auxiliaries;
using StoryForge.Core.AI.Chat;
using StoryForge.Core.Utils;

namespace StoryForge.Core.AI.Providers;

/// <summary>
/// Low-level client that talks to a specific model instance on a provider.
/// Implementations adapt provider SDKs.
/// </summary>
public interface ILLMClient
{
    string ProviderId { get; }
    string ModelId { get; }

    Task<Result<Completion>> CompleteAsync(IReadOnlyList<Message> messages, CancellationToken cancellationToken = default);

    Task StreamCompleteAsync(IReadOnlyList<Message> messages, IStreamingObserver observer, CancellationToken cancellationToken = default);

    Task<Result<Embedding>> GetEmbeddingsAsync(IReadOnlyList<string> inputs, CancellationToken cancellationToken = default);

    /// <summary>
    /// Optional: returns provider-specific model metadata or capabilities.
    /// </summary>
    Task<ModelInfo> GetModelInfoAsync(CancellationToken cancellationToken = default);
}
