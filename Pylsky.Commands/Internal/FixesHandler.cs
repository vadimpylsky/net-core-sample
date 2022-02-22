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
    IRequestHandler<DeleteCommand, Guid>
{
    private readonly IFixesRepository _fixesRepository;
    private readonly IPylskyLogger<FixesHandler> _logger;
    private readonly IUsersRepository _usersRepository;

    public FixesHandler(
        IPylskyLogger<FixesHandler> logger,
        IFixesRepository fixesRepository,
        IUsersRepository usersRepository)
    {
        _logger = logger;
        _fixesRepository = fixesRepository;
        _usersRepository = usersRepository;
    }

    public Task<Guid> Handle(AddFixCommand request, CancellationToken cancellationToken)
    {
        _logger.Info("Handle(AddFixCommand request, CancellationToken cancellationToken)");

        var model = new Fix(
            _fixesRepository,
            request.DeveloperId,
            request.Link,
            request.CreatedAt);

        return model.SaveAsync();
    }

    public async Task<UserModel> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        _logger.Info("Handle(CreateUserCommand request, CancellationToken cancellationToken)");

        var id = await _usersRepository
            .CreateUserAsync(request.Id, request.Name)
            .ConfigureAwait(false);

        return new UserModel(id, request.Name);
    }

    public async Task<Guid> Handle(DeleteCommand request, CancellationToken cancellationToken)
    {
        _logger.Info("Handle(DeleteCommand request, CancellationToken cancellationToken)");

        await _fixesRepository
            .DeleteFixAsync(request.Id)
            .ConfigureAwait(false);

        return request.Id;
    }
}