using StoryForge.Core.Services;
using StoryForge.Core.Utils;

namespace StoryForge.Application.Services;

public class ProjectFileStorage : IProjectFileStorage
{
    private const string DefaultDirectory = "./projects";

    public string CreateProjectPath(string projectName)
    {
        EnsureDirectoryExist();
        return Path.GetFullPath(Path.Combine(DefaultDirectory, $"{projectName}.db"));
    }

    public async Task<Result<string>> SaveProjectFileAsync(string fileName, Stream stream, CancellationToken cancellationToken = default)
    {
        try
        {
            EnsureDirectoryExist();
            var targetPath = Path.Combine(DefaultDirectory, fileName);

            // If stream supports seeking and is at 0 position, ensure it's rewound
            if (stream.CanSeek && stream.Position != 0) stream.Seek(0, SeekOrigin.Begin);

            using var fileStream = File.Create(targetPath);
            await stream.CopyToAsync(fileStream, cancellationToken);
            await fileStream.FlushAsync(cancellationToken);

            return Result<string>.Ok(Path.GetFullPath(targetPath));
        }
        catch (Exception ex)
        {
            return Result<string>.Fail(ex.Message);
        }
        finally
        {
            stream.Dispose();
        }
    }

    void EnsureDirectoryExist()
    {
        if (!Directory.Exists(DefaultDirectory))
        {
            Directory.CreateDirectory(DefaultDirectory);
        }
    }
}
