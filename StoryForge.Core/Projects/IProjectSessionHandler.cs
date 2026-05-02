using StoryForge.Core.Utils;

namespace StoryForge.Core.Projects;

public interface IProjectSessionHandler : IAsyncDisposable
{
    bool IsActive { get; }
    string? CurrentProject { get; }

    Task<Result> LoadSession(string filePath, CancellationToken ct = default);
    Task<Result> StartSession(Project project, CancellationToken ct = default);
    Task StopSession();
}
