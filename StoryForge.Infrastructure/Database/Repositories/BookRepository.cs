using StoryForge.Core.Data;
using StoryForge.Core.Repositories;
using StoryForge.Core.Utils;

namespace StoryForge.Infrastructure.Database.Repositories;

internal class BookRepository : IBookRepository
{
    protected readonly ISummaryRepository summaries;
    protected Book? book;

    public BookRepository(ISummaryRepository summaryRepository)
    {
        summaries = summaryRepository;
    }

    public Result<Book> Get()
    {
        if (book is null) return Result<Book>.Fail("Book not found");
        return Result<Book>.Ok(book);
    }

    public void Update(Book updatedBook)
    {
        book = updatedBook;
        summaries.Update(updatedBook.Extra.Summary);
    }

    public void Reset()
    {
        book = null;
    }
}
