namespace StoryForge.Simulator.Utils.Commands;

public static class CommandExtension
{
    public static void NoArgument(this CommandData command)
    {
        Console.WriteLine($"{Environment.NewLine}No argument for {command.Name} provided{Environment.NewLine}");
    }

    public static void UnknownArgument(this CommandData command, string arg)
    {
        Console.WriteLine($"{Environment.NewLine}Unknown argument {arg} for {command.Name}{Environment.NewLine}");
    }
}
