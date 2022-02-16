using System;

namespace Pylsky.Infrastructure.Ef.Entities;

public class BugEntity
{
    public BugEntity(string id, string link, DateTimeOffset createdAt)
    {
        Id = id;
        Link = link;
        CreatedAt = createdAt;
    }

    public string Id { get; private set; }

    public string Link { get; private set; }
    
    public DateTimeOffset CreatedAt { get; private set; }
}