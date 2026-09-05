using Keystone;
using Microsoft.EntityFrameworkCore;
using StoryForge.Core.Projects;
using StoryForge.Core.Storage.Repositories;

namespace StoryForge.Infrastructure.Database.SQLite.Repositories;

internal class ProjectInfoRepository : IProjectInfoRepository
{
    protected readonly DbSet<ProjectInfo> meta;

    public ProjectInfoRepository(ProjectDbContext context)
    {
        meta = context.Meta;
    }

    public Result<string> Get(ProjectMeta category)
    {
        return meta
            .SingleOrDefault(info => info.Category == category)
            .AsOption()
            .Map(info => info.Value)
            .ToResult($"{category} not found");
    }

    public void Set(ProjectMeta category, string value)
    {
        meta.SingleOrDefault(info => info.Category == category)
            .AsOption()
            .Match(
                info =>
                {
                    meta.Update(new ProjectInfo(category, value));
                    return true;
                },
                () =>
                {
                    meta.Add(new ProjectInfo(category, value));
                    return true;
                });
    }

    public void Delete(ProjectMeta category)
    {
        meta.SingleOrDefault(info => info.Category == category)
            .AsOption()
            .DoWith(info => meta.Remove(info));
    }
}
