using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pylsky.Core.Interfaces;
using Pylsky.Core.Models;

namespace Pylsky.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ActivitiesController : ControllerBase
{
    private readonly IActivitiesService _activitiesService;
    private readonly IPylskyLogger<ActivitiesController> _logger;

    public ActivitiesController(
        IActivitiesService activitiesService,
        IPylskyLogger<ActivitiesController> logger)
    {
        _activitiesService = activitiesService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IList<Activity>>> Get()
    {
        _logger.Log("get activities");
        var result = await _activitiesService.GetActivitiesAsync().ConfigureAwait(false);
        return new ActionResult<IList<Activity>>(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Activity>> GetItem(string id)
    {
        _logger.Log("get activity");
        var item = await _activitiesService.GetAsync(id).ConfigureAwait(false);
        return new ActionResult<Activity>(item);
    }

    [HttpPost]
    public async Task<ActionResult<Activity>> Create(string name)
    {
        _logger.Log("create activity");
        var result = await _activitiesService.CreateActivityAsync(name).ConfigureAwait(false);
        return CreatedAtAction(nameof(GetItem), new {id = result.Id}, result);
    }
}