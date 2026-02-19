using StoryForge.Core.Data;
using StoryForge.Core.Utils;

namespace StoryForge.Core.Storage.Repositories;

public interface IBookRepository
{
    Result<Book> Get();
    void Update(Book book);
    void Reset();
}
