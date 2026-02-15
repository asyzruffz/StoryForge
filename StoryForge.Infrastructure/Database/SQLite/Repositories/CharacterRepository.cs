using Microsoft.EntityFrameworkCore;
using StoryForge.Core.Data;
using StoryForge.Core.Repositories;
using StoryForge.Core.Utils;

namespace StoryForge.Infrastructure.Database.SQLite.Repositories;

internal class CharacterRepository : ICharacterRepository
{
    protected readonly DbSet<Character> characters;

    public CharacterRepository(ProjectDbContext context)
    {
        characters = context.Characters;
    }

    public IQueryable<Character> GetAll()
    {
        return characters.AsQueryable();
    }

    public Result<Character> GetById(CharacterId id)
    {
        return characters
            .SingleOrDefault(character => character.Id == id)
            .AsOption().ToResult();
    }

    public void Create(Character character)
    {
        characters.Add(character);
    }

    public void Create(IEnumerable<Character> character)
    {
        characters.AddRange(character);
    }

    public void Update(Character character)
    {
        characters.Update(character);
    }

    public void Delete(Character character)
    {
        characters.Remove(character);
    }
}
