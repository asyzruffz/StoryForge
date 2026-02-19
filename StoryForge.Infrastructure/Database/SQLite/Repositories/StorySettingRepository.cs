using Microsoft.EntityFrameworkCore;
using StoryForge.Core.Data;
using StoryForge.Core.Storage.Repositories;
using StoryForge.Core.Utils;

namespace StoryForge.Infrastructure.Database.SQLite.Repositories;

internal class StorySettingRepository : IStorySettingRepository
{
    protected readonly DbSet<StorySetting> settings;

    public StorySettingRepository(ProjectDbContext context)
    {
        settings = context.StorySettings;
    }

    public IQueryable<StorySetting> GetAll()
    {
        return settings.AsQueryable();
    }

    public Result<StorySetting> GetById(StorySettingId id)
    {
        return settings
            .SingleOrDefault(setting => setting.Id == id)
            .AsOption().ToResult();
    }

    public void Create(StorySetting setting)
    {
        settings.Add(setting);
    }

    public void Create(IEnumerable<StorySetting> setting)
    {
        settings.AddRange(setting);
    }

    public void Update(StorySetting setting)
    {
        settings.Update(setting);
    }

    public void Delete(StorySetting setting)
    {
        settings.Remove(setting);
    }
}
