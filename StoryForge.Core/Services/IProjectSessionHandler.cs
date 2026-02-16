using StoryForge.Core.Data;
using StoryForge.Core.Utils;

namespace StoryForge.Core.Services;

public interface IProjectSessionHandler : IDisposable
{
    bool IsActive { get; }
    string? CurrentProject { get; }
    Result StartSession(Project project);
    void StopSession();
}
