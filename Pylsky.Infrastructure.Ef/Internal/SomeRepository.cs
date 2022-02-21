using System;
using System.Threading.Tasks;
using Pylsky.Core.Interfaces;
using Pylsky.Infrastructure.Ef.Entities;

namespace Pylsky.Infrastructure.Ef.Internal;

internal class SomeRepository : ISomeRepository
{
    private readonly DatabaseContext _databaseContext;

    public SomeRepository(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public async Task<Guid> SaveFixAsync(
        Guid developerId,
        string bugId,
        string link,
        DateTimeOffset createdAt,
        DateTimeOffset fixedAt)
    {
        await _databaseContext.Bugs
            .AddAsync(new BugEntity(bugId, link, createdAt))
            .ConfigureAwait(false);

        var fix = await _databaseContext.Fixes
            .AddAsync(new FixEntity(developerId, bugId, fixedAt))
            .ConfigureAwait(false);

        await _databaseContext.SaveChangesAsync().ConfigureAwait(false);

        return fix.Entity.Id;
    }

    public async Task<Guid> CreateUserAsync(string id, string name)
    {
        var entry = await _databaseContext.Developers
            .AddAsync(new DeveloperEntity(id, name))
            .ConfigureAwait(false);

        await _databaseContext.SaveChangesAsync().ConfigureAwait(false);

        return entry.Entity.Id;
    }

    public async Task DeleteAsync(string id)
    {
        var entity = await _databaseContext.Bugs
            .FindAsync(id)
            .ConfigureAwait(false);

        _databaseContext.Bugs.Remove(entity!);
        await _databaseContext.SaveChangesAsync().ConfigureAwait(false);
    }
}