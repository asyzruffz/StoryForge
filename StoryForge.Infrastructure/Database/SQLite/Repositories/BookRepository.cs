using Microsoft.EntityFrameworkCore;
using StoryForge.Core.Data;
using StoryForge.Core.Repositories;
using StoryForge.Core.Utils;

namespace StoryForge.Infrastructure.Database.SQLite.Repositories;

internal class BookRepository : IBookRepository
{
    protected readonly DbSet<Book> books;

    public BookRepository(ProjectDbContext context)
    {
        books = context.Books;
    }

    public Result<Book> Get()
    {
        try
        {
            var book = books
                .Include(book => book.Extra)
                .ThenInclude(extra => extra.Summary)
                .Single();
            return Result<Book>.Ok(book);
        }
        catch (Exception ex)
        {
            return Result<Book>.Fail($"{ex.Message}");
        }
    }

    public void Update(Book book)
    {
        Get().Match(existingBook =>
        {
            if (existingBook.Id != book.Id)
            {
                book.Id = existingBook.Id;
            }

            books.Update(book);
            return true;
        },
        _ =>
        {
            books.Add(book);
            return false;
        });
    }

    public void Reset()
    {
        books.RemoveRange(books);
    }
}
