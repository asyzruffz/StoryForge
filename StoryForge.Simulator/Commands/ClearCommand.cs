using MediatR;
using StoryForge.Simulator.Utils.Commands;

namespace StoryForge.Simulator.Commands;

public class ClearCommand : CommandBase
{
    public const string Name = "clear";

    public override Func<CommandData, CancellationToken, Task> Action => Execute;

    public ClearCommand(ISender sender) : base(sender) { }

    private async Task Execute(CommandData command, CancellationToken cancellationToken)
    {
        Console.Clear();
        await Task.CompletedTask;
    }
}
