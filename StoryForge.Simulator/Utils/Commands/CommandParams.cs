using System.Text;

namespace StoryForge.Simulator.Utils.Commands;

public static class CommandParams
{
    public static IEnumerable<string> Parse(string input)
    {
        //string rawInput = input.Trim();
        //return rawInput.Split(' ', options: StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
        return split(input.Trim(), ' ');
    }

    private static IEnumerable<string> split(string stringToSplit, params char[] separators)
    {
        bool inQuote = false;
        StringBuilder currentToken = new StringBuilder();
        for (int index = 0; index < stringToSplit.Length; ++index)
        {
            char currentCharacter = stringToSplit[index];
            if (currentCharacter == '"')
            {
                // When we see a ", we need to decide whether we are
                // at the start or send of a quoted section...
                inQuote = !inQuote;
            }
            else if (separators.Contains(currentCharacter) && !inQuote)
            {
                // We've come to the end of a token, so we find the token,
                // trim it and add it to the collection of results...
                string result = currentToken.ToString().Trim();
                if (result != "") yield return result;

                // We start a new token...
                currentToken = new StringBuilder();
            }
            else
            {
                // We've got a 'normal' character, so we add it to
                // the curent token...
                currentToken.Append(currentCharacter);
            }
        }

        // We've come to the end of the string, so we add the last token...
        string lastResult = currentToken.ToString().Trim();
        if (lastResult != "") yield return lastResult;
    }
}
