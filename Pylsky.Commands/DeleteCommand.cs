using System;
using MediatR;

namespace Pylsky.Commands;

public class DeleteCommand : IRequest<Guid>
{
    public DeleteCommand(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; }
}