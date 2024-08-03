using MediatR;
using StoryForge.Simulator.Utils.Commands;

namespace StoryForge.Simulator.Commands;

public class VersionCommand : CommandBase
{
    public override string Name => "version";
    public override Func<List<string>, Task> Action => Execute;

    public VersionCommand(ISender sender) : base(sender) { }

    private async Task Execute(List<string> param)
    {
        Message("Story Forge v1.0");
        await Task.CompletedTask;
    }
}
