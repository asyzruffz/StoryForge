using StoryForge.Core.Repositories;

namespace StoryForge.Core.Services;

public interface IApplicationDataSession : IDisposable
{
    IProjectRepository Projects { get; }
    int Save();
}
