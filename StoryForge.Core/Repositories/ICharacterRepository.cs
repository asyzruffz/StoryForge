using StoryForge.Core.Data;

namespace StoryForge.Core.Repositories;

public interface ICharacterRepository : IRepository<Character>, IQueryableById<Character, CharacterId>;
