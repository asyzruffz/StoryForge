using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoryForge.Core.Data;

namespace StoryForge.Infrastructure.Database.SQLite.Configurations;

internal class SummaryConfiguration : IEntityTypeConfiguration<Summary>
{
    public void Configure(EntityTypeBuilder<Summary> builder)
    {
        builder.HasKey(summary => summary.Id);

        builder.Property(summary => summary.Id)
            .HasConversion(id => id.Value, value => SummaryId.From(value))
            .ValueGeneratedNever();
    }
}
