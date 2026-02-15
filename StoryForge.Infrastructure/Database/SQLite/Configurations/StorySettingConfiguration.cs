using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoryForge.Core.Data;

namespace StoryForge.Infrastructure.Database.SQLite.Configurations;

internal class StorySettingConfiguration : IEntityTypeConfiguration<StorySetting>
{
    public void Configure(EntityTypeBuilder<StorySetting> builder)
    {
        builder.HasKey(setting => setting.Id);

        builder.Property(setting => setting.Id)
            .HasConversion(id => id.Value, value => StorySettingId.From(value));

        builder.Property(setting => setting.Category)
            .HasConversion(category => category.Name, value => new StorySettingCategory(value));
    }
}
