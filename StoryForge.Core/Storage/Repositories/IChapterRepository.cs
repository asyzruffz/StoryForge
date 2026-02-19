using StoryForge.Core.Data;

namespace StoryForge.Core.Storage.Repositories;

public interface IChapterRepository : IRepository<Chapter>, IQueryableById<Chapter, ChapterId>;
