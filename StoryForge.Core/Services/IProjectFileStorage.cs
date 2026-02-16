using StoryForge.Core.Utils;

namespace StoryForge.Core.Services;

public interface IProjectFileStorage
{
    string CreateProjectPath(string projectName);
    Task<Result<string>> SaveProjectFileAsync(string fileName, Stream stream, CancellationToken cancellationToken = default);
}
