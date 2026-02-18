using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoryForge.Core.Data;

namespace StoryForge.Infrastructure.Database.SQLite.Configurations;

internal class ChapterConfiguration : IEntityTypeConfiguration<Chapter>
{
    public void Configure(EntityTypeBuilder<Chapter> builder)
    {
        builder.HasKey(chapter => chapter.Id);

        builder.Property(chapter => chapter.Id)
            .HasConversion(id => id.Value, value => ChapterId.From(value))
            .ValueGeneratedNever();

        builder.Property(chapter => chapter.Title)
            .HasConversion(title => title.Content, value => new Title(value));

        builder.Ignore(chapter => chapter.Prev);
        builder.Ignore(chapter => chapter.Next);

        builder.Property(chapter => chapter.Content)
            .HasConversion(content => content.ToText(), value => new ContentSection(value));
    }
}
