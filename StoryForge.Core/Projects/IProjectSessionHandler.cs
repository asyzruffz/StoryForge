using Keystone;

namespace StoryForge.Core.Projects;

public interface IProjectSessionHandler : IAsyncDisposable
{
    bool IsActive { get; }
    string? CurrentProject { get; }

    ValueTask<Result> LoadSession(string filePath, CancellationToken ct = default);
    ValueTask<Result> StartSession(Project project, CancellationToken ct = default);
    ValueTask StopSession();
}
