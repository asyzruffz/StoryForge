using StoryForge.Core.Data;
using StoryForge.Core.Repositories;
using StoryForge.Core.Utils;

namespace StoryForge.Infrastructure.Repositories;

internal class ChapterRepository : IChapterRepository
{
    //protected readonly DbSet<Chapter> chapters;
    protected readonly List<Chapter> chapters;

    public ChapterRepository(ApplicationDbContext context)
    {
        chapters = context.Chapters;
    }

    public IQueryable<Chapter> GetAll()
    {
        return chapters.AsQueryable();
    }

    public Result<Chapter> GetById(ChapterId id)
    {
        return chapters
            .SingleOrDefault(chapter => chapter.Id == id)
            .AsOption().ToResult();
    }

    public void Create(Chapter chapter)
    {
        chapters.Add(chapter);
    }

    public void Create(IEnumerable<Chapter> chapter)
    {
        chapters.AddRange(chapter);
    }

    public void Update(Chapter chapter)
    {
        throw new NotImplementedException();
    }

    public void Delete(Chapter chapter)
    {
        chapters.Remove(chapter);
    }
}
