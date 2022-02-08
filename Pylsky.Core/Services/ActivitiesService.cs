using System.Collections.Generic;
using System.Threading.Tasks;
using Pylsky.Core.Interfaces;
using Pylsky.Core.Models;

namespace Pylsky.Core.Services;

internal class ActivitiesService : IActivitiesService
{
    private readonly IActivitiesDataSource _activitiesDataSource;
    private readonly IPylskyLogger<ActivitiesService> _logger;

    public ActivitiesService(
        IPylskyLogger<ActivitiesService> logger,
        IActivitiesDataSource activitiesDataSource)
    {
        _logger = logger;
        _activitiesDataSource = activitiesDataSource;
    }

    public async Task<IList<Activity>> GetActivitiesAsync()
    {
        _logger.Log("get activities");
        var result = await _activitiesDataSource.GetAllAsync().ConfigureAwait(false);

        return result;
    }

    public async Task<Activity> CreateActivityAsync(string name)
    {
        _logger.Log("create activity");
        var result = await _activitiesDataSource.CreateAsync(name).ConfigureAwait(false);

        return result;
    }

    public async Task<Activity> GetAsync(string id)
    {
        _logger.Log("get activity");
        var result = await _activitiesDataSource.GetAsync(id);

        return result;
    }
}