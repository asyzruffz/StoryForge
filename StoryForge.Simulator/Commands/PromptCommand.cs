using MediatR;
using StoryForge.Application.AIGeneration;
using StoryForge.Simulator.Utils.Commands;

namespace StoryForge.Simulator.Commands;

public class PromptCommand : CommandBase
{
    public override string Name => "prompt";
    public override Func<CommandData, Task> Action => Execute;

    public PromptCommand(ISender sender) : base(sender) { }

    private async Task Execute(CommandData command)
    {
        if (!command.ParamIsAtLeast(1))
        {
            command.NoArgument();
            return;
        }

        Message(string.Empty);
        var result = await Sender.Send(new GenerateWithPromptOperation(command.Params[0]));
        result.OnError(Message).Then(Message);
    }
}
