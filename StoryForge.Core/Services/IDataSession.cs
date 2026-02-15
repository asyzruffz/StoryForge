using StoryForge.Core.Repositories;

namespace StoryForge.Core.Services;

public interface IDataSession : IDisposable
{
    ISummaryRepository Summaries { get; }
    ICharacterRepository Characters { get; }
    IPlotRepository Plots { get; }
    IStorySettingRepository StorySettings { get; }
    IChapterRepository Chapters { get; }
    int Save();
}
