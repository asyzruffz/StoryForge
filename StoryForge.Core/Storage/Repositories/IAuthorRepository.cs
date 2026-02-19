using StoryForge.Core.Data;
using StoryForge.Core.Utils;

namespace StoryForge.Core.Storage.Repositories;

public interface IAuthorRepository
{
    Result<Author> Get();
    void Update(Author author);
    void Reset();
}
