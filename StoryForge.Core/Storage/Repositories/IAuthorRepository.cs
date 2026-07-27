using Keystone;
using StoryForge.Core.Data;

namespace StoryForge.Core.Storage.Repositories;

public interface IAuthorRepository
{
    Result<Author> Get();
    void Update(Author author);
    void Reset();
}
