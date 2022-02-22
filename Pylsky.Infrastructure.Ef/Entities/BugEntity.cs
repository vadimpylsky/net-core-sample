using System;
using Microsoft.EntityFrameworkCore;

namespace Pylsky.Infrastructure.Ef.Entities;

[Index(nameof(Link))]
public class BugEntity
{
    public BugEntity(string link, DateTimeOffset createdAt)
    {
        Link = link;
        CreatedAt = createdAt;
    }

    public Guid Id { get; private set; }

    public string Link { get; private set; }
    
    public DateTimeOffset CreatedAt { get; private set; }
}