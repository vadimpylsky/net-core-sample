using System.Linq;
using Pylsky.Infrastructure.Ef.Entities;

namespace Pylsky.Infrastructure.Ef;

public interface IQueryableAggregate
{
    IQueryable<BugEntity> Bugs { get; }
    IQueryable<DeveloperEntity> Developers { get; }
    IQueryable<FixEntity> Fixes { get; }
}