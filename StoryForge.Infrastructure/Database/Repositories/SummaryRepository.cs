using StoryForge.Core.Data;
using StoryForge.Core.Repositories;
using StoryForge.Core.Utils;

namespace StoryForge.Infrastructure.Database.Repositories;

internal class SummaryRepository : ISummaryRepository
{
    protected BookSummary? bookSummary;

    public Result<BookSummary> Get()
    {
        if (bookSummary is null) return Result<BookSummary>.Fail("Summary not found");
        return Result<BookSummary>.Ok(bookSummary);
    }

    public void Update(BookSummary summary)
    {
        bookSummary = summary;
    }

    public void Reset()
    {
        bookSummary = null;
    }
}
