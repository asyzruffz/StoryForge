using StoryForge.Core.Data;

namespace StoryForge.Core.Repositories;

public interface ISummaryRepository : IRepository<Summary>, IQueryableById<Summary, SummaryId>
{
}
