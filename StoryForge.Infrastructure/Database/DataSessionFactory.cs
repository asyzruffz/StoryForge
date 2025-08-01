﻿using StoryForge.Core.Services;

namespace StoryForge.Infrastructure.Database;

public class DataSessionFactory : IDataSessionFactory
{
    private readonly ApplicationDbContext context;

    public DataSessionFactory(ApplicationDbContext context) => this.context = context;

    public IDataSession CreateSession() => new DataSession(context);
}
