using StoryForge.Core.Data;
using StoryForge.Core.Storage;

namespace StoryForge.Core.Projects;

public class Project
{
    public string FilePath { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public DateTime LastActive { get; set; } = DateTime.UtcNow;
    public bool IsFavourite { get; set; } = false;

    public DateTime LastActiveLocal => LastActive.ToLocalTime();

    public void SetActive()
    {
        LastActive = DateTime.UtcNow;
    }

    public async Task RegisterToAppAsync(IApplicationDataSession appData, CancellationToken ct)
    {
        appData.Projects.Create(this);
        await appData.SaveAsync(ct).ConfigureAwait(false);
    }

    public async Task InitializeAsync(IDataSession dataSession, CancellationToken ct)
    {
        dataSession.Meta.Set(ProjectMeta.Name, Name);
        dataSession.Books.Update(new Book
        {
            Id = BookId.New(),
            Title = Name,
            Extra = BookSummary.New()
        });
        dataSession.Authors.Update(new Author { Id = AuthorId.New() });

        await dataSession.SaveAsync(ct).ConfigureAwait(false);
    }
}
