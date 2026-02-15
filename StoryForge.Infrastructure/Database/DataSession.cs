using StoryForge.Core.Repositories;
using StoryForge.Core.Services;
using StoryForge.Infrastructure.Database.SQLite;
using StoryForge.Infrastructure.Database.SQLite.Repositories;

namespace StoryForge.Infrastructure.Database;

public class DataSession : IDataSession, IDisposable
{
    private readonly ProjectDbContext context;

    public ISummaryRepository Summaries { get; init; }
    public ICharacterRepository Characters { get; init; }
    public IPlotRepository Plots { get; init; }
    public IStorySettingRepository StorySettings { get; init; }
    public IChapterRepository Chapters { get; init; }

    public DataSession(ProjectDbContext context)
    {
        this.context = context;

        Summaries = new SummaryRepository(context);
        Characters = new CharacterRepository(context);
        Plots = new PlotRepository(context);
        StorySettings = new StorySettingRepository(context);
        Chapters = new ChapterRepository(context);
    }

    public bool EnsureCreated() => context.Database.EnsureCreated();
    public int Save() => context.SaveChanges();
    public void Dispose() => context.Dispose();
}
