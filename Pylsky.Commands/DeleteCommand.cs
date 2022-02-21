using MediatR;

namespace Pylsky.Commands;

public class DeleteCommand : IRequest<string>
{
    public DeleteCommand(string id)
    {
        Id = id;
    }

    public string Id { get; }
}