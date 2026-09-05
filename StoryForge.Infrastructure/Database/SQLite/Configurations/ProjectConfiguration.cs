using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoryForge.Core.Projects;

namespace StoryForge.Infrastructure.Database.SQLite.Configurations;

internal class ProjectConfiguration : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.HasKey(project => project.FilePath);

        builder.Property(project => project.FilePath)
            .ValueGeneratedNever();
    }
}
