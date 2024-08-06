namespace StoryForge.Simulator.Utils.Commands;

public static class CommandParams
{
    public static IEnumerable<string> Parse(string input)
    {
        string rawInput = input.Trim();
        return rawInput.Split(' ', options: StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
    }
}
