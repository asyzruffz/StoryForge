using StoryForge.Core.Services;

namespace StoryForge.Infrastructure;

public class DataSession : IDataSession, IDisposable
{
    //private readonly ApplicationDbContext context;

    public DataSession(/*ApplicationDbContext context*/)
    {
        /*this.context = context;

        Users = new UserRepository(context);
        Characters = new CharacterRepository(context);
        Titles = new TitleRepository(context);
        Items = new ItemRepository(context);
        Inventories = new InventoryRepository(context);
        Skills = new SkillRepository(context);
        Proficiencies = new SkillProficiencyRepository(context);
        Relationships = new RelationshipRepository(context);
        Posts = new PostRepository(context);
        Locations = new LocationRepository(context);
        Activities = new ActivityRepository(context);*/
    }

    /*public IUserRepository Users { get; init; }
    public ICharacterRepository Characters { get; init; }
    public ITitleRepository Titles { get; init; }
    public IItemRepository Items { get; init; }
    public IInventoryRepository Inventories { get; init; }
    public ISkillRepository Skills { get; init; }
    public ISkillProficiencyRepository Proficiencies { get; init; }
    public IRelationshipRepository Relationships { get; init; }
    public IPostRepository Posts { get; init; }
    public ILocationRepository Locations { get; init; }
    public IActivityRepository Activities { get; init; }*/

    public int Save() => 0;// context.SaveChanges();
    public void Dispose() { }// => context.Dispose();
}
