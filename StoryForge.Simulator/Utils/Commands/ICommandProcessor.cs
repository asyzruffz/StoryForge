namespace StoryForge.Simulator.Utils.Commands;

public interface ICommandProcessor
{
    bool IsRunning { get; }
    Task Read(string? input, CancellationToken cancellationToken);
}
