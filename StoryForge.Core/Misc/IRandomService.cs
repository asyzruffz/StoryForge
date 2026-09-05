namespace StoryForge.Core.Misc;

public interface IRandomService
{
    int Next(int maxValue);
    Random SystemRandomizer { get; }
}
