using System.Linq;
using Pylsky.Infrastructure.Ef.Entities;

namespace Pylsky.Infrastructure.Ef.Internal;

internal class QueryableAggregate : IQueryableAggregate
{
    public QueryableAggregate(DatabaseContext databaseContext)
    {
        Bugs = databaseContext.Bugs.AsQueryable();
        Developers = databaseContext.Developers.AsQueryable();
        Fixes = databaseContext.Fixes.AsQueryable();
    }

    public IQueryable<BugEntity> Bugs { get; }
    public IQueryable<DeveloperEntity> Developers { get; }
    public IQueryable<FixEntity> Fixes { get; }
}