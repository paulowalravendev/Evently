using Evently.Modules.Events.Api.Database;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;

namespace Evently.Modules.Events.Api.Events;

public static class GetEvent
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("events/{id:guid}", async (Guid id, EventsDbContext context) =>
        {
            EventResponse? response = await context.Events
                .Where(e => e.Id == id)
                .Select(e => new EventResponse(e.Id, e.Title, e.Description, e.Location, e.StartsAtUtc, e.EndsAtUtc, e.Status))
                .SingleOrDefaultAsync();

            return response is null ? Results.NotFound() : Results.Ok(response);
        })
            .WithTags(Tags.Events);
    }
}
