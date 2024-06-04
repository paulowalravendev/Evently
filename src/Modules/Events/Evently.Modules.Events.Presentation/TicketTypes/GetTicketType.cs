using MediatR;
using Evently.Common.Domain;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Evently.Modules.Events.Application.TicketTypes.GetTicketType;
using Evently.Common.Presentation.Endpoints;
using Evently.Common.Presentation.ApiResults;

namespace Evently.Modules.Events.Presentation.TicketTypes;

internal sealed class GetTicketType : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("ticket-types/{id}", async (Guid id, ISender sender) =>
        {
            Result<TicketTypeResponse> result = await sender.Send(new GetTicketTypeQuery(id));

            return result.Match(Results.Ok, ApiResults.Problem);
        })
        .RequireAuthorization()
        .WithTags(Tags.TicketTypes);
    }
}
