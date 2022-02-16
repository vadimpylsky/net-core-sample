using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pylsky.Api.Dtos;
using Pylsky.Commands;
using Pylsky.Core.Interfaces;
using Pylsky.Queries;

namespace Pylsky.Api.Controllers;

[ApiController]
[Authorize]
[Route("[controller]")]
public class BugsController : ControllerBase
{
    private readonly IPylskyLogger<BugsController> _logger;
    private readonly IMediator _mediator;
    private readonly IQueries _queries;

    public BugsController(
        IPylskyLogger<BugsController> logger,
        IMediator mediator,
        IQueries queries)
    {
        _logger = logger;
        _mediator = mediator;
        _queries = queries;
    }

    [HttpGet(nameof(Feed))]
    public async Task<IActionResult> Feed()
    {
        var items = await _queries.GetInfosAsync().ConfigureAwait(false);
        return Ok(items);
    }

    [HttpPost(nameof(Create))]
    public async Task<IActionResult> Create(FixDto fixDto)
    {
        var userId = HttpContext.User.Claims.First(x => x.Type == "user_id").Value;
        var user = await _queries.GetUserAsync(userId).ConfigureAwait(false);

        if (user == null)
        {
            var userName = HttpContext.User.Identity!.Name!;

            user = await _mediator
                .Send(new CreateUserCommand(userId, userName))
                .ConfigureAwait(false);
        }

        await _mediator
            .Send(new AddFixCommand(user.Id, fixDto.Link, fixDto.CreatedAt))
            .ConfigureAwait(false);

        return Ok();
    }
}