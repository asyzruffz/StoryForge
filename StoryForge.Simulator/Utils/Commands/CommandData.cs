namespace StoryForge.Simulator.Utils.Commands;

public class CommandData
{
    public string Name
    {
        get { return tokens.Count > 0 ? tokens[0].ToLower() : string.Empty; }
    }

    public List<string> Params
    {
        get { return tokens.Count - 1 > 0 ? tokens.GetRange(1, tokens.Count - 1) : []; }
    }

    List<string> tokens;

    public CommandData(IEnumerable<string> param)
    {
        tokens = param.ToList();
    }

    public bool ParamIsAtLeast(int amount)
    {
        return Params.Count >= amount;
    }
}
