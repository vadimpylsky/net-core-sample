using System;
using System.Threading.Tasks;
using Pylsky.Core.Interfaces;
using Pylsky.Infrastructure.Ef.Entities;

namespace Pylsky.Infrastructure.Ef.Internal;

internal class UsersRepository : IUsersRepository
{
    private readonly DatabaseContext _databaseContext;
    private readonly IPylskyLogger<UsersRepository> _logger;

    public UsersRepository(
        IPylskyLogger<UsersRepository> logger,
        DatabaseContext databaseContext)
    {
        _logger = logger;
        _databaseContext = databaseContext;
    }

    public async Task<Guid> CreateUserAsync(string id, string name)
    {
        _logger.Info($"CreateUserAsync({id}, {name}");
        
        var entry = await _databaseContext.Developers
            .AddAsync(new DeveloperEntity(id, name))
            .ConfigureAwait(false);

        await _databaseContext
            .SaveChangesAsync()
            .ConfigureAwait(false);

        return entry.Entity.Id;
    }
}