using StoryForge.Core.Data;

namespace StoryForge.Core.Storage.Repositories;

public interface ISummaryRepository : IRepository<Summary>, IQueryableById<Summary, SummaryId>;
