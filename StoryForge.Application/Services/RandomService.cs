﻿using StoryForge.Core.Services;

namespace StoryForge.Application.Services;

public class RandomService : IRandomService
{
    public Random SystemRandomizer => random;
    private readonly Random random;

    public RandomService()
    {
        random = new Random();
    }

    public int Next(int maxValue)
    {
        return random.Next(maxValue);
    }
}
