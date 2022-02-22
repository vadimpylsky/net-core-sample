using System;
using System.Threading.Tasks;

namespace Pylsky.Core.Interfaces;

public interface IFixesRepository
{
    public Task<Guid> SaveFixAsync(
        Guid developerId,
        string link,
        DateTimeOffset createdAt,
        DateTimeOffset fixedAt);

    Task DeleteFixAsync(Guid id);
}