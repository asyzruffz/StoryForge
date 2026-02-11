using StoryForge.Core.Repositories;

namespace StoryForge.Core.Services;

public interface IDataSession : IDisposable
{
    IBookRepository Books { get; }
    IAuthorRepository Authors { get; }
    ISummaryRepository Summaries { get; }
    ICharacterRepository Characters { get; }
    IChapterRepository Chapters { get; }
    int Save();
}
