using System;
using System.Threading.Tasks;

namespace Pylsky.Core.Interfaces;

public interface ISomeRepository
{
    public Task<Guid> SaveFixAsync(
        Guid developerId,
        string bugId,
        string link,
        DateTimeOffset createdAt,
        DateTimeOffset fixedAt);

    Task<Guid> CreateUserAsync(string id, string name);

    Task DeleteAsync(string requestId);
}