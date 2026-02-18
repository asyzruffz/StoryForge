using StoryForge.Core.Repositories;

namespace StoryForge.Core.Services;

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
