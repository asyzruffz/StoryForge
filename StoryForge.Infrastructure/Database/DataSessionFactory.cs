using StoryForge.Core.Services;
using StoryForge.Infrastructure.Database.InMemory;

namespace StoryForge.Infrastructure.Database;

public class DataSessionFactory : IDataSessionFactory
{
    private readonly ProjectDbContext context;

    public DataSessionFactory(ProjectDbContext context) => this.context = context;

    public IDataSession CreateSession() => new DataSession(context);
}
