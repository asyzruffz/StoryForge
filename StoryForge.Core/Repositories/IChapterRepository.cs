using StoryForge.Core.Data;

namespace StoryForge.Core.Repositories;

public interface IChapterRepository : IRepository<Chapter>, IQueryableById<Chapter, ChapterId>
{
}
