using System;

namespace Pylsky.Infrastructure.Ef.Entities;

public class FixEntity
{
    public FixEntity(Guid developerId, string bugId, DateTimeOffset fixedAt)
    {
        DeveloperId = developerId;
        BugId = bugId;
        FixedAt = fixedAt;
    }

    public Guid Id { get; private set; }

    public Guid DeveloperId { get; private set; }

    public string BugId { get; private set; }

    public DateTimeOffset FixedAt { get; private set; }
}