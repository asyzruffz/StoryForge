using Keystone;
using StoryForge.Core.Data;
using StoryForge.Core.Storage.Repositories;

namespace StoryForge.Infrastructure.Database.InMemory.Repositories;

internal class ChapterRepository : IChapterRepository
{
    protected readonly List<Chapter> chapters;

    public ChapterRepository(ProjectDbContext context)
    {
        chapters = context.Chapters;
    }

    public IQueryable<Chapter> GetAll()
    {
        return chapters.AsQueryable();
    }

    public Option<Chapter> GetById(ChapterId id)
    {
        return chapters
            .SingleOrDefault(chapter => chapter.Id == id)
            .AsOption();
    }

    public bool HasWithId(ChapterId id) =>
        chapters.Any(chapter => chapter.Id == id);

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
        var foundChapter = chapters.SingleOrDefault(entry => entry.Id == chapter.Id);
        if (foundChapter is null) return;

        int idx = chapters.IndexOf(foundChapter);
        chapters[idx] = chapter;
    }

    public void Delete(Chapter chapter)
    {
        chapters.Remove(chapter);
    }
}
