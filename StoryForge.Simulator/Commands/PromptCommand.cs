using MediatR;
using StoryForge.Application.AIGeneration;
using StoryForge.Core.Utils;
using StoryForge.Simulator.Utils;
using StoryForge.Simulator.Utils.Commands;

namespace StoryForge.Simulator.Commands;

public class PromptCommand : CommandBase
{
    public const string Name = "prompt";

    public override Func<CommandData, CancellationToken, Task> Action => Execute;

    public PromptCommand(ISender sender) : base(sender) { }

    private async Task Execute(CommandData command, CancellationToken cancellationToken)
    {
        if (!command.ParamIsAtLeast(1))
        {
            command.NoArgument();
            return;
        }

        Message(string.Empty);

        CancellationTokenSource cts = new();
        Task loading = LoadingSpinner.Wait(cts.Token);

        Task<Result<string>> operation = Sender.Send(new GenerateWithPromptOperation(command.Params[0]), cancellationToken);
        await operation.ContinueWith(op => cts.Cancel(), cancellationToken);

        Console.Write("\r");
        operation.Result.OnError(MessageLine).Then(MessageLine);
    }
}
