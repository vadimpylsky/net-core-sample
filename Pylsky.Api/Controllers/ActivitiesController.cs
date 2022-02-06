using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pylsky.Core;
using Pylsky.Core.Logger;

namespace Pylsky.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ActivitiesController : ControllerBase
{
    private readonly ActivitiesService _activitiesService;
    private readonly IPylskyLogger<ActivitiesController> _logger;

    public ActivitiesController(ActivitiesService activitiesService, IPylskyLogger<ActivitiesController> logger)
    {
        _activitiesService = activitiesService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IList<Activity>> Get()
    {
        _logger.Log("get activities from ActivitiesController...");
        var result = await _activitiesService.GetActivitiesAsync().ConfigureAwait(false);
        return result;
    }
}