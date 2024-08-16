using StoryForge.Core.Repositories;
using StoryForge.Core.Services;
using StoryForge.Infrastructure.Database.Repositories;

namespace StoryForge.Infrastructure.Database;

public class DataSession : IDataSession, IDisposable
{
    private readonly ApplicationDbContext context;

    public DataSession(ApplicationDbContext context)
    {
        this.context = context;

        Chapters = new ChapterRepository(context);
    }

    public IChapterRepository Chapters { get; init; }

    public int Save() => context.SaveChanges();
    public void Dispose() => context.Dispose();
}
