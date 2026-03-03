using StoryForge.Core.Utils;

namespace StoryForge.Core.Projects;

public interface IProjectSessionHandler : IAsyncDisposable
{
    bool IsActive { get; }
    string? CurrentProject { get; }

    Task<Result> StartSession(Project project, bool newlyCreated = false, CancellationToken ct = default);
    Task StopSession(CancellationToken ct = default);
}
