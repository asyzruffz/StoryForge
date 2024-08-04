using MediatR;
using StoryForge.Simulator.Utils.Commands;

namespace StoryForge.Simulator.Commands;

public class ClearCommand : CommandBase
{
    public override string Name => "clear";
    public override Func<CommandData, Task> Action => Execute;

    public ClearCommand(ISender sender) : base(sender) { }

    private async Task Execute(CommandData command)
    {
        Console.Clear();
        await Task.CompletedTask;
    }
}
