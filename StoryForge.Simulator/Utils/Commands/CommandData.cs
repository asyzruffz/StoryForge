namespace StoryForge.Simulator.Utils.Commands;

class CommandData
{
    public string Name
    {
        get { return tokens.Count > 0 ? tokens[0].ToLower() : string.Empty; }
    }

    public List<string> Params
    {
        get { return tokens.Count - 1 > 0 ? tokens.GetRange(1, tokens.Count - 1) : new List<string>(); }
    }

    List<string> tokens = new List<string>();

    public CommandData(string rawInput)
    {
        string input = rawInput.Trim();
        tokens = input.Split(' ', options: StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).ToList();
    }

    public CommandData(List<string> param)
    {
        tokens = param;
    }

    public bool ParamIsAtLeast(int amount)
    {
        return Params.Count >= amount;
    }
}
