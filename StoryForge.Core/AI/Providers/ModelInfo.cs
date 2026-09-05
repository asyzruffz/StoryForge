namespace StoryForge.Core.AI.Providers;

public sealed record ModelInfo(string ProviderId, string ModelId, string DisplayName, string? Description, int? MaxTokens, bool SupportsStreaming, bool SupportsEmbeddings);
