using Microsoft.EntityFrameworkCore;
using StoryForge.Core.Data;
using StoryForge.Core.Storage.Repositories;
using StoryForge.Core.Utils;

namespace StoryForge.Infrastructure.Database.SQLite.Repositories;

internal class ChapterRepository : IChapterRepository
{
    protected readonly DbSet<Chapter> chapters;

    public ChapterRepository(ProjectDbContext context)
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
        chapters.Update(chapter);
    }

    public void Delete(Chapter chapter)
    {
        chapters.Remove(chapter);
    }
}
