using Evently.Common.Presentation.Endpoints;
using Microsoft.Extensions.DependencyInjection;

namespace Evently.Modules.Ticketing.Infrastructure;

public static class TicketingModule
{
    public static IServiceCollection AddTicketingModule(
        this IServiceCollection services)
    {
        services.AddEndpoints(Presentation.AssemblyReference.Assembly);

        return services;
    }
}
