using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoryForge.Core.Projects;

namespace StoryForge.Infrastructure.Database.SQLite.Configurations;

internal class ProjectInfoConfiguration : IEntityTypeConfiguration<ProjectInfo>
{
    public void Configure(EntityTypeBuilder<ProjectInfo> builder)
    {
        builder.HasKey(info => info.Category);

        builder.Property(info => info.Category)
            .HasConversion(id => id.ToString(), value => Enum.Parse<ProjectMeta>(value))
            .ValueGeneratedNever();

        builder.Property(info => info.Value);
    }
}
