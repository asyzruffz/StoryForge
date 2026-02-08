using StoryForge.Core.Data;
using StoryForge.Core.Utils;

namespace StoryForge.Core.Repositories;

public interface ISummaryRepository : IRepository<Summary>, IQueryableById<Summary, SummaryId>
{
}
