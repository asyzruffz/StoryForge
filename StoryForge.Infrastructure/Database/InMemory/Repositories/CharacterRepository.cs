using StoryForge.Core.Data;
using StoryForge.Core.Storage.Repositories;
using StoryForge.Core.Utils;

namespace StoryForge.Infrastructure.Database.InMemory.Repositories;

internal class CharacterRepository : ICharacterRepository
{
    protected readonly List<Character> characters;
    protected readonly List<Summary> summaries;

    public CharacterRepository(ProjectDbContext context)
    {
        characters = context.Characters;
        summaries = context.Summaries;
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
        summaries.Add(character.Summary);
    }

    public void Create(IEnumerable<Character> character)
    {
        characters.AddRange(character);
        summaries.AddRange(character.Select(c => c.Summary));
    }

    public void Update(Character character)
    {
        var foundCharacter = characters.SingleOrDefault(entry => entry.Id == character.Id);
        if (foundCharacter is null) return;

        int idx = characters.IndexOf(foundCharacter);
        characters[idx] = character;
    }

    public void Delete(Character character)
    {
        characters.Remove(character);
    }
}
