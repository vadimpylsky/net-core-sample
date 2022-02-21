using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Pylsky.Core.Models;
using Pylsky.Infrastructure.Ef;
using Pylsky.Queries.Dtos;

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
        var data = await _queryableAggregate.Fixes
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
                (x, y) => new {x.Name, y.Link, y.CreatedAt, x.FixedAt})
            .ToListAsync().ConfigureAwait(false);

        int GetPoints(DateTimeOffset x, DateTimeOffset y)
        {
            var diff = (x - y).TotalDays;

            if (diff < 1d)
            {
                diff = 1d;
            }

            return (int) Math.Abs(diff);
        }

        var startDate = DateTime.UtcNow.Date;

        while (startDate.DayOfWeek != DayOfWeek.Monday)
        {
            startDate = startDate.AddDays(-1);
        }

        var offset = new DateTimeOffset(startDate);

        var items = data
            .Where(x => x.FixedAt > offset)
            .OrderByDescending(x => x.FixedAt)
            .Select(x => new FixInfoDto(x.Name, x.Link, GetPoints(x.FixedAt, x.CreatedAt)))
            .ToList();

        return items;
    }

    public async Task<List<FixEntityDto>> GetUserFixesAsync(Guid userId)
    {
        var data = await _queryableAggregate.Fixes
            .Where(x => x.DeveloperId == userId)
            .Join(_queryableAggregate.Bugs,
                x => x.BugId,
                y => y.Id,
                (x, y) => new FixEntityDto(x.BugId, y.Link, x.FixedAt))
            .ToListAsync().ConfigureAwait(false);

        var items = data.OrderByDescending(x => x.FixedAt).ToList();

        return items;
    }
}