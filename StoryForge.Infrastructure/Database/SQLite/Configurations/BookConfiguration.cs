using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoryForge.Core.Data;

namespace StoryForge.Infrastructure.Database.SQLite.Configurations;

internal class BookConfiguration : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.HasKey(book => book.Id);

        builder.Property(book => book.Id)
            .HasConversion(id => id.Value, value => BookId.From(value))
            .ValueGeneratedNever();

        builder.Property(book => book.Title);

        builder.Property(book => book.Subtitle).IsRequired(false);
        builder.Property(book => book.Series).IsRequired(false);
        builder.Property(book => book.Volume).IsRequired(false);
        builder.Property(book => book.Genre).IsRequired(false);

        builder.OwnsOne(book => book.Extra, book =>
        {
            book.Property(extra => extra.Situation);
            book.Property(extra => extra.SummaryId)
                .HasConversion(id => id.Value, value => SummaryId.From(value));
            book.HasOne(extra => extra.Summary)
                .WithMany()
                .HasForeignKey(extra => extra.SummaryId)
                .IsRequired();
        });
    }
}
