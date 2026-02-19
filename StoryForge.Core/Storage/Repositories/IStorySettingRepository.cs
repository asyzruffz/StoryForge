using StoryForge.Core.Data;

namespace StoryForge.Core.Storage.Repositories;

public interface IStorySettingRepository : IRepository<StorySetting>, IQueryableById<StorySetting, StorySettingId>;
