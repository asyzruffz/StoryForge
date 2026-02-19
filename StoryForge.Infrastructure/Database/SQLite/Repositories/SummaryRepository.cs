using Microsoft.EntityFrameworkCore;
using StoryForge.Core.Data;
using StoryForge.Core.Storage.Repositories;
using StoryForge.Core.Utils;

namespace StoryForge.Infrastructure.Database.SQLite.Repositories;

internal class SummaryRepository : ISummaryRepository
{
    protected readonly DbSet<Summary> summaries;

    public SummaryRepository(ProjectDbContext context)
    {
        summaries = context.Summaries;
    }

    public IQueryable<Summary> GetAll()
    {
        return summaries.AsQueryable();
    }

    public Result<Summary> GetById(SummaryId id)
    {
        return summaries
            .SingleOrDefault(summary => summary.Id == id)
            .AsOption().ToResult();
    }

    public void Create(Summary summary)
    {
        summaries.Add(summary);
    }

    public void Create(IEnumerable<Summary> summary)
    {
        summaries.AddRange(summary);
    }

    public void Update(Summary summary)
    {
        summaries.Update(summary);
    }

    public void Delete(Summary summary)
    {
        summaries.Remove(summary);
    }
}
