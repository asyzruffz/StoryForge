using StoryForge.Core.Repositories;

namespace StoryForge.Core.Services;

public interface IDataSession : IDisposable
{
    IChapterRepository Chapters { get; }
    int Save();
}
