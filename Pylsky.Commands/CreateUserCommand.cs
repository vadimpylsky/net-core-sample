using MediatR;
using Pylsky.Core.Models;

namespace Pylsky.Commands;

public class CreateUserCommand : IRequest<UserModel>
{
    public CreateUserCommand(string id, string name)
    {
        Id = id;
        Name = name;
    }

    public string Id { get; }
    
    public string Name { get; }
}