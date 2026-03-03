using StoryForge.Core.Storage.Repositories;

namespace StoryForge.Core.Storage;

public interface IDataSession : IAsyncDisposable
{
    IBookRepository Books { get; }
    IAuthorRepository Authors { get; }
    ISummaryRepository Summaries { get; }
    ICharacterRepository Characters { get; }
    IPlotRepository Plots { get; }
    IStorySettingRepository StorySettings { get; }
    IChapterRepository Chapters { get; }

    Task<bool> EnsureCreatedAsync(CancellationToken ct);
    Task<int> SaveAsync(CancellationToken ct);
}
