using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoryForge.Core.Data;

namespace StoryForge.Infrastructure.Database.SQLite.Configurations;

internal class CharacterConfiguration : IEntityTypeConfiguration<Character>
{
    public void Configure(EntityTypeBuilder<Character> builder)
    {
        builder.HasKey(character => character.Id);

        builder.Property(character => character.Id)
            .HasConversion(id => id.Value, value => CharacterId.From(value))
            .ValueGeneratedNever();
    }
}
