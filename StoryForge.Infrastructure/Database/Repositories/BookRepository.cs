using StoryForge.Core.Data;
using StoryForge.Core.Repositories;
using StoryForge.Core.Utils;

namespace StoryForge.Infrastructure.Database.Repositories;

internal class BookRepository : IBookRepository
{
    protected Book? book;

    public Result<Book> Get()
    {
        if (book is null) return Result<Book>.Fail("Book not found");
        return Result<Book>.Ok(book);
    }

    public void Update(Book updatedBook)
    {
        book = updatedBook;
    }

    public void Reset()
    {
        book = null;
    }
}
