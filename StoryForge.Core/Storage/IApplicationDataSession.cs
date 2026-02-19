using StoryForge.Core.Storage.Repositories;

namespace StoryForge.Core.Storage;

public interface IApplicationDataSession : IDisposable
{
    IProjectRepository Projects { get; }

    bool EnsureCreated();
    int Save();
}
