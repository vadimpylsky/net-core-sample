using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Pylsky.Core.Interfaces;
using Pylsky.Core.Models;

namespace Pylsky.Infrastructure.Ef.SqLite;

internal class ActivitiesContext : DbContext, IActivitiesDataSource
{
    private readonly IPylskyLogger<ActivitiesContext> _logger;
    private DbSet<ActivityEntity> Activities => Set<ActivityEntity>();

    public ActivitiesContext(
        IPylskyLogger<ActivitiesContext> logger,
        SqLiteContextOptions sqLiteContextOptions) : base(sqLiteContextOptions.DbContextOptions)
    {
        _logger = logger;
    }

    public async Task<Activity> CreateAsync(string name)
    {
        _logger.Log("create activity");

        var entity = new ActivityEntity(Guid.NewGuid(), name);
        Activities.Add(entity);
        await SaveChangesAsync().ConfigureAwait(false);

        return Map(entity);
    }

    public async Task<IList<Activity>> GetAllAsync()
    {
        _logger.Log("get activities");
        var entities = await Activities.ToListAsync().ConfigureAwait(false);
        var models = entities.Select(Map).ToList();

        return models;
    }

    public async Task<Activity> GetAsync(string id)
    {
        _logger.Log("get activity");
        var result = await Activities.FirstAsync(x => x.Id == Guid.Parse(id));

        return Map(result);
    }

    private Activity Map(ActivityEntity x)
    {
        return new Activity(x.Id.ToString(), x.Name);
    }
}