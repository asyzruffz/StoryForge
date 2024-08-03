namespace StoryForge.Core.Services;

public interface IRandomService
{
    int Next(int maxValue);
    Random SystemRandomizer { get; }
}
