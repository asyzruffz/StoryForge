using MediatR;
using StoryForge.Application.Chapters;
using StoryForge.Simulator.Utils.Commands;

namespace StoryForge.Simulator.Commands;

public class ChapterCommand : CommandBase
{
    public const string Name = "chapter";

    public override Func<CommandData, CancellationToken, Task> Action => Execute;

    public ChapterCommand(ISender sender) : base(sender) { }

    private async Task Execute(CommandData command, CancellationToken cancellationToken)
    {
        if (!command.ParamIsAtLeast(1))
        {
            command.NoArgument();
            return;
        }

        var subcommand = new CommandData(command.Params);
        switch (subcommand.Name)
        {
            case "list": await ListChapters(subcommand, cancellationToken); break;
            case "add": await AddChapter(subcommand, cancellationToken); break;
            case "delete": await DeleteChapter(subcommand, cancellationToken); break;
            case "link": await LinkChapters(subcommand, cancellationToken); break;
            default: command.UnknownArgument(subcommand.Name); break;
        }

        await Task.CompletedTask;
    }

    private async Task ListChapters(CommandData command, CancellationToken cancellationToken)
    {
        Message(string.Empty);
        var result = await Sender.Send(new GetChaptersOperation(), cancellationToken);
        result.OnError(MessageLine).Then(chapters =>
        {
            if (chapters.Count() == 0)
            {
                MessageLine("No chapters available");
                return;
            }

            foreach (var chapter in chapters)
            {
                Message($"[{chapter.Id.Value.Substring(0, 8)}] {chapter.Title}");
            }
            Message(string.Empty);
        });
    }

    private async Task AddChapter(CommandData command, CancellationToken cancellationToken)
    {
        var title = command.ParamIsAtLeast(1) ? command.Params[0] : "Untitled";
        var result = await Sender.Send(new AddChapterOperation(title), cancellationToken);

        Message(string.Empty);
        result.OnError(MessageLine).Then(() =>
        {
            MessageLine("Chapter added sucessfully");
        });
    }

    private async Task DeleteChapter(CommandData command, CancellationToken cancellationToken)
    {
        if (!command.ParamIsAtLeast(1))
        {
            command.NoArgument();
            return;
        }

        var result = await Sender.Send(new DeleteChapterOperation(command.Params[0]), cancellationToken);

        Message(string.Empty);
        result.OnError(MessageLine).Then(() =>
        {
            MessageLine("Chapter deleted sucessfully");
        });
    }

    private async Task LinkChapters(CommandData command, CancellationToken cancellationToken)
    {
        if (!command.ParamIsAtLeast(2))
        {
            command.NoArgument();
            return;
        }

        var result = await Sender.Send(new LinkChapterOperation(command.Params[0], command.Params[1]), cancellationToken);

        Message(string.Empty);
        result.OnError(MessageLine).Then(() =>
        {
            MessageLine("Chapters linked sucessfully");
        });
    }
}
