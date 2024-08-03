namespace StoryForge.Core.Services;

public interface IDataSession : IDisposable
{
    /*IUserRepository Users { get; }
    ICharacterRepository Characters { get; }
    ITitleRepository Titles { get; }
    IItemRepository Items { get; }
    IInventoryRepository Inventories { get; }
    ISkillRepository Skills { get; }
    ISkillProficiencyRepository Proficiencies { get; }
    IRelationshipRepository Relationships { get; }
    IPostRepository Posts { get; }
    ILocationRepository Locations { get; }
    IActivityRepository Activities { get; }*/
    int Save();
}
