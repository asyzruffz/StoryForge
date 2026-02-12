using StoryForge.Core.Data;

namespace StoryForge.Core.Repositories;

public interface IStorySettingRepository : IRepository<StorySetting>, IQueryableById<StorySetting, StorySettingId>
{
}
