using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoryForge.Core.Data;

namespace StoryForge.Infrastructure.Database.SQLite.Configurations;

internal class ProjectConfiguration : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.HasKey(project => project.Id);

        builder.Property(project => project.Id)
            .HasConversion(id => id.Value, value => ProjectId.From(value));

        builder.Property(project => project.FilePath);

        builder.OwnsOne(project => project.Book, project =>
        {
            project.Property(book => book.Title);
            project.Property(book => book.Subtitle).IsRequired(false);
            project.Property(book => book.Series).IsRequired(false);
            project.Property(book => book.Volume).IsRequired(false);
            project.Property(book => book.Genre).IsRequired(false);
            project.OwnsOne(book => book.Extra, book =>
            {
                book.Property(extra => extra.Situation);
                book.Property(extra => extra.SummaryId)
                    .HasConversion(id => id.Value, value => SummaryId.From(value));
                book.Ignore(extra => extra.Summary);
            });
        });

        builder.OwnsOne(project => project.Author, author =>
        {
            author.Property(a => a.Name);
            author.Property(a => a.Email).IsRequired(false);
        });
    }
}
