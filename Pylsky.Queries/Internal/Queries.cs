using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Pylsky.Core.Models;
using Pylsky.Infrastructure.Ef;
using Pylsky.Queries.dtos;

namespace Pylsky.Queries.Internal;

internal class Queries : IQueries
{
    private readonly IQueryableAggregate _queryableAggregate;

    public Queries(IQueryableAggregate queryableAggregate)
    {
        _queryableAggregate = queryableAggregate;
    }

    public async Task<UserModel?> GetUserAsync(string id)
    {
        var entity = await _queryableAggregate.Developers
            .FirstOrDefaultAsync(x => x.ExternalId == id)
            .ConfigureAwait(false);

        return entity == null
            ? null
            : new UserModel(entity.Id, entity.Name);
    }

    public async Task<List<FixInfoDto>> GetInfosAsync()
    {
        var items = await _queryableAggregate.Fixes
            .Join(_queryableAggregate.Developers,
                fix => fix.DeveloperId,
                dev => dev.Id, (x, y) => new
                {
                    y.Name,
                    x.FixedAt,
                    x.BugId
                })
            .Join(_queryableAggregate.Bugs,
                x => x.BugId,
                y => y.Id,
                (x, y) => new FixInfoDto(x.Name, y.Link, 100))
            .ToListAsync();

        return items;
    }
}