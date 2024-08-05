using StoryForge.Core.Data;
using StoryForge.Core.Repositories;
using StoryForge.Core.Utils;

namespace StoryForge.Infrastructure.Repositories;

public class ChapterRepository : IChapterRepository
{
    //protected readonly DbSet<Chapter> chapters;
    protected readonly List<Chapter> chapters;

    public ChapterRepository(ApplicationDbContext context)
    {
        chapters = context.Chapters;
    }

    public IQueryable<Chapter> GetAll()
    {
        throw new NotImplementedException();
    }

    public Result<Chapter> GetById(ChapterId id)
    {
        throw new NotImplementedException();
    }

    public void Create(Chapter obj)
    {
        throw new NotImplementedException();
    }

    public void Create(IEnumerable<Chapter> objs)
    {
        throw new NotImplementedException();
    }

    public void Update(Chapter obj)
    {
        throw new NotImplementedException();
    }

    public void Delete(Chapter obj)
    {
        throw new NotImplementedException();
    }
}
