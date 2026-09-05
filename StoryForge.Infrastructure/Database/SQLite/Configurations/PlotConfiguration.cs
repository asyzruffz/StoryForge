using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoryForge.Core.Data;

namespace StoryForge.Infrastructure.Database.SQLite.Configurations;

internal class PlotConfiguration : IEntityTypeConfiguration<Plot>
{
    public void Configure(EntityTypeBuilder<Plot> builder)
    {
        builder.HasKey(plot => plot.Id);

        builder.Property(plot => plot.Id)
            .HasConversion(id => id.Value, value => PlotId.From(value))
            .ValueGeneratedNever();
    }
}
