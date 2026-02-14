using StoryForge.Core.Repositories;
using StoryForge.Core.Services;
using StoryForge.Infrastructure.Database.InMemory;
using StoryForge.Infrastructure.Database.InMemory.Repositories;

namespace StoryForge.Infrastructure.Database;

public class DataSession : IDataSession, IDisposable
{
    private readonly ProjectDbContext context;

    public DataSession(ProjectDbContext context)
    {
        this.context = context;

        Summaries = new SummaryRepository(context);
        Books = new BookRepository(Summaries);
        Authors = new AuthorRepository();
        Characters = new CharacterRepository(context);
        Plots = new PlotRepository(context);
        StorySettings = new StorySettingRepository(context);
        Chapters = new ChapterRepository(context);
    }

    public IBookRepository Books { get; init; }
    public IAuthorRepository Authors { get; init; }
    public ISummaryRepository Summaries { get; init; }
    public ICharacterRepository Characters { get; init; }
    public IPlotRepository Plots { get; init; }
    public IStorySettingRepository StorySettings { get; init; }
    public IChapterRepository Chapters { get; init; }

    public int Save() => context.SaveChanges();
    public void Dispose() => context.Dispose();
}
