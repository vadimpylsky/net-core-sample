using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Pylsky.Core.Interfaces;
using Pylsky.Core.Models;

namespace Pylsky.Commands.Internal;

internal class FixesHandler :
    IRequestHandler<AddFixCommand, Guid>,
    IRequestHandler<CreateUserCommand, UserModel>,
    IRequestHandler<DeleteCommand, string>
{
    private readonly ISomeRepository _someRepository;

    public FixesHandler(ISomeRepository someRepository)
    {
        _someRepository = someRepository;
    }

    public Task<Guid> Handle(AddFixCommand request, CancellationToken cancellationToken)
    {
        var model = new Fix(
            _someRepository,
            request.DeveloperId,
            request.Link,
            request.CreatedAt);

        return model.SaveAsync();
    }

    public async Task<UserModel> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var id = await _someRepository
            .CreateUserAsync(request.Id, request.Name)
            .ConfigureAwait(false);

        return new UserModel(id, request.Name);
    }

    public async Task<string> Handle(DeleteCommand request, CancellationToken cancellationToken)
    {
        await _someRepository
            .DeleteAsync(request.Id)
            .ConfigureAwait(false);

        return request.Id;
    }
}