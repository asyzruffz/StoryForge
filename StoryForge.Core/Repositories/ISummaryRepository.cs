using StoryForge.Core.Data;
using StoryForge.Core.Utils;

namespace StoryForge.Core.Repositories;

public interface ISummaryRepository
{
    Result<BookSummary> Get();
    void Update(BookSummary summary);
    void Reset();
}
