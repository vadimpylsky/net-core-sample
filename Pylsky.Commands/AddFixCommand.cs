using System;
using MediatR;

namespace Pylsky.Commands;

public class AddFixCommand : IRequest<Guid>
{
    public AddFixCommand(Guid developerId, string link, DateTimeOffset createdAt)
    {
        DeveloperId = developerId;
        Link = link;
        CreatedAt = createdAt;
    }

    public Guid DeveloperId { get; }

    public string Link { get; }

    public DateTimeOffset CreatedAt { get; }
}