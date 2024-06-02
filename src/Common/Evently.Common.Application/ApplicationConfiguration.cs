using System.Reflection;
using Evently.Common.Application.Behaviors;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Evently.Common.Application;

public static class ApplicationConfiguration
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services,
        Assembly[] modulesAssemblies)
    {
        services.AddMediatR(cfg => {
            cfg.RegisterServicesFromAssemblies(modulesAssemblies);

            cfg.AddOpenBehavior(typeof(ExceptionHandlingPipelineBehavior<,>));
            cfg.AddOpenBehavior(typeof(RequestLoggingPipelineBehavior<,>));
            cfg.AddOpenBehavior(typeof(ValidationPipelineBehavior<,>));
        });

        services.AddValidatorsFromAssemblies(modulesAssemblies,includeInternalTypes: true);

        return services;
    }
}
