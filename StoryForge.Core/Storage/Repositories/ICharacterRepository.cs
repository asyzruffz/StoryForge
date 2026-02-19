using StoryForge.Core.Data;

namespace StoryForge.Core.Storage.Repositories;

public interface ICharacterRepository : IRepository<Character>, IQueryableById<Character, CharacterId>;
