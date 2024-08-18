namespace StoryForge.Simulator.Utils;

internal static class LoadingSpinner
{
    public static async Task Wait(CancellationToken cancellationToken)
    {
        int counter = 0;
        while (!cancellationToken.IsCancellationRequested)
        {
            counter++;
            switch (counter % 4)
            {
                case 0: Console.Write("\r/ "); counter = 0; break;
                case 1: Console.Write("\r- "); break;
                case 2: Console.Write("\r\\ "); break;
                case 3: Console.Write("\r| "); break;
            }

            await Task.Delay(200, cancellationToken);
        }
    }
}
