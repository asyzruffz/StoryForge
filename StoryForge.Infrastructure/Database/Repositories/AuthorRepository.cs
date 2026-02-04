using StoryForge.Core.Data;
using StoryForge.Core.Repositories;
using StoryForge.Core.Utils;

namespace StoryForge.Infrastructure.Database.Repositories;

internal class AuthorRepository : IAuthorRepository
{
    protected Author? author;

    public Result<Author> Get()
    {
        if (author is null) return Result<Author>.Fail("Author not found");
        return Result<Author>.Ok(author);
    }

    public void Update(Author updatedAuthor)
    {
        author = updatedAuthor;
    }

    public void Reset()
    {
        author = null;
    }
}
