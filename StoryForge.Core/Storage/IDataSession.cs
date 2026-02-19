using StoryForge.Core.Storage.Repositories;

namespace StoryForge.Core.Storage;

public interface IDataSession : IDisposable
{
    IBookRepository Books { get; }
    IAuthorRepository Authors { get; }
    ISummaryRepository Summaries { get; }
    ICharacterRepository Characters { get; }
    IPlotRepository Plots { get; }
    IStorySettingRepository StorySettings { get; }
    IChapterRepository Chapters { get; }

    bool EnsureCreated();
    int Save();
}
