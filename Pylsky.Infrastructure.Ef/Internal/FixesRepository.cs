using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Pylsky.Core.Interfaces;
using Pylsky.Infrastructure.Ef.Entities;

namespace Pylsky.Infrastructure.Ef.Internal;

internal class FixesRepository : IFixesRepository
{
    private readonly IPylskyLogger<FixesRepository> _logger;
    private readonly DatabaseContext _databaseContext;

    public FixesRepository(
        IPylskyLogger<FixesRepository> logger,
        DatabaseContext databaseContext)
    {
        _logger = logger;
        _databaseContext = databaseContext;
    }

    public async Task<Guid> SaveFixAsync(
        Guid developerId,
        string link,
        DateTimeOffset createdAt,
        DateTimeOffset fixedAt)
    {
        _logger.Info($"SaveFixAsync({developerId}, {link}, {createdAt}, {fixedAt})");

        var existedBug = await _databaseContext.Bugs
            .FirstOrDefaultAsync(x => x.Link == link)
            .ConfigureAwait(false);

        if (existedBug != null)
        {
            throw new ValidationException();
        }

        var bug = await _databaseContext.Bugs
            .AddAsync(new BugEntity(link, createdAt))
            .ConfigureAwait(false);

        var fix = await _databaseContext.Fixes
            .AddAsync(new FixEntity(developerId, bug.Entity.Id, fixedAt))
            .ConfigureAwait(false);

        await _databaseContext
            .SaveChangesAsync()
            .ConfigureAwait(false);

        return fix.Entity.Id;
    }

    public async Task DeleteFixAsync(Guid id)
    {
        _logger.Info($"DeleteFixAsync({id})");
        
        var entity = await _databaseContext.Bugs
            .FindAsync(id)
            .ConfigureAwait(false);

        _databaseContext.Bugs.Remove(entity!);
        
        await _databaseContext
            .SaveChangesAsync()
            .ConfigureAwait(false);
    }
}