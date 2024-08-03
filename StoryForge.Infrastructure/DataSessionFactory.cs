using StoryForge.Core.Services;

namespace StoryForge.Infrastructure;

public class DataSessionFactory : IDataSessionFactory
{
    public DataSessionFactory() { }

    public IDataSession CreateSession() => new DataSession();
}
