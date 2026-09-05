using Keystone;
using StoryForge.Core.Data;

namespace StoryForge.Core.Storage.Repositories;

public interface IBookRepository
{
    Result<Book> Get();
    void Update(Book book);
    void Reset();
}
