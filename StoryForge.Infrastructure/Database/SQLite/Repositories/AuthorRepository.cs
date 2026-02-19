using Microsoft.EntityFrameworkCore;
using StoryForge.Core.Data;
using StoryForge.Core.Storage.Repositories;
using StoryForge.Core.Utils;

namespace StoryForge.Infrastructure.Database.SQLite.Repositories;

internal class AuthorRepository : IAuthorRepository
{
    protected readonly DbSet<Author> authors;

    public AuthorRepository(ProjectDbContext context)
    {
        authors = context.Authors;
    }

    public Result<Author> Get()
    {
        try
        {
            return Result<Author>.Ok(authors.Single());
        }
        catch (Exception ex)
        {
            return Result<Author>.Fail($"{ex.Message}");
        }
    }

    public void Update(Author author)
    {
        Get().Match(existingAuthor =>
        {
            if (existingAuthor.Id != author.Id)
            {
                author.Id = existingAuthor.Id;
            }

            authors.Update(author);
            return true;
        },
        _ =>
        {
            authors.Add(author);
            return false;
        });
    }

    public void Reset()
    {
        authors.RemoveRange(authors);
    }
}
