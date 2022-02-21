using System;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Pylsky.Api.Extensions;
using Pylsky.Commands;
using Pylsky.Core.Interfaces;
using Pylsky.Queries;

namespace Pylsky.Api.Middlewares;

internal class MapToInternalUserMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IServiceResolver _serviceResolver;

    public MapToInternalUserMiddleware(RequestDelegate next, IServiceResolver serviceResolver)
    {
        _next = next ?? throw new ArgumentNullException(nameof(next));
        _serviceResolver = serviceResolver;
    }

    public async Task Invoke(HttpContext context)
    {
        var userId = context.User.Claims.First(x => x.Type == "user_id").Value;
        var queries = _serviceResolver.Resolve<IQueries>();
        var user = await queries.GetUserAsync(userId).ConfigureAwait(false);

        if (user == null)
        {
            var userName = context.User.Identity!.Name!;
            var mediator = _serviceResolver.Resolve<IMediator>();

            user = await mediator
                .Send(new CreateUserCommand(userId, userName))
                .ConfigureAwait(false);
        }

        context.Items.AddUserGuid(user.Id);

        await _next.Invoke(context);
    }
}