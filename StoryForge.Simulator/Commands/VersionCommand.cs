using MediatR;
using StoryForge.Simulator.Utils.Commands;

namespace StoryForge.Simulator.Commands;

public class VersionCommand : CommandBase
{
    public const string Name = "version";

    public override Func<CommandData, CancellationToken, Task> Action => Execute;

    public VersionCommand(ISender sender) : base(sender) { }

    private async Task Execute(CommandData command, CancellationToken cancellationToken)
    {
        MessageLine("Story Forge v1.0");
        await Task.CompletedTask;
    }
}
