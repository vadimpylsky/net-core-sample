using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pylsky.Api.Dtos;
using Pylsky.Api.Extensions;
using Pylsky.Commands;
using Pylsky.Queries;
using Pylsky.Queries.Dtos;

namespace Pylsky.Api.Controllers;

[ApiController]
[Authorize]
[Route("[controller]")]
public class BugsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IQueries _queries;

    public BugsController(
        IMediator mediator,
        IQueries queries)
    {
        _mediator = mediator;
        _queries = queries;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<FixEntityDto>), (int) HttpStatusCode.OK)]
    public async Task<IActionResult> Get()
    {
        var userId = HttpContext.Items.GetUserGuid();
        var items = await _queries.GetUserFixesAsync(userId).ConfigureAwait(false);
        return Ok(items);
    }

    [HttpGet(nameof(Feed))]
    [ProducesResponseType(typeof(IEnumerable<FixInfoDto>), (int) HttpStatusCode.OK)]
    public async Task<IActionResult> Feed()
    {
        var items = await _queries.GetInfosAsync().ConfigureAwait(false);
        return Ok(items);
    }

    [HttpPost(nameof(Create))]
    public async Task<IActionResult> Create(FixDto fixDto)
    {
        var userId = HttpContext.Items.GetUserGuid();

        await _mediator
            .Send(new AddFixCommand(userId, fixDto.Link, fixDto.CreatedAt))
            .ConfigureAwait(false);

        return Ok();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Remove(Guid id)
    {
        await _mediator
            .Send(new DeleteCommand(id))
            .ConfigureAwait(false);

        return Ok();
    }
}