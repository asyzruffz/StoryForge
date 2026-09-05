using StoryForge.Core.Storage.Repositories;

namespace StoryForge.Core.Storage;

public interface IApplicationDataSession : IAsyncDisposable
{
    IProjectRepository Projects { get; }

    Task<bool> EnsureCreatedAsync(CancellationToken ct);
    Task<int> SaveAsync(CancellationToken ct);
}
