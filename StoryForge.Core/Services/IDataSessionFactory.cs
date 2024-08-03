namespace StoryForge.Core.Services;

public interface IDataSessionFactory
{
    IDataSession CreateSession();
}
