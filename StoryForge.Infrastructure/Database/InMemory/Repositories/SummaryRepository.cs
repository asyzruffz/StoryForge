using StoryForge.Core.Data;
using StoryForge.Core.Repositories;
using StoryForge.Core.Utils;

namespace StoryForge.Infrastructure.Database.InMemory.Repositories;

internal class SummaryRepository : ISummaryRepository
{
    protected readonly List<Summary> summaries;

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
        var foundSummary = summaries.SingleOrDefault(entry => entry.Id == summary.Id);
        if (foundSummary is null) return;

        int idx = summaries.IndexOf(foundSummary);
        summaries[idx] = summary;
    }

    public void Delete(Summary summary)
    {
        summaries.Remove(summary);
    }
}
