using System.Reflection;

using FluentValidation;

using MediatR;

using Microsoft.Extensions.DependencyInjection;

using Sample_CleanArchitecture_CQRS.Application.Common.Behaviors;
using Sample_CleanArchitecture_CQRS.Application.Configuration;

namespace Sample_CleanArchitecture_CQRS.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationService(this IServiceCollection services)
    {
        services.AddMediatR(
            config =>
            {
                config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());

                config.AddOpenBehavior(typeof(RequestValidationBehavior<,>));
            });


        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));

        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly, includeInternalTypes: true);

        services.AddApplicationMapping();


        return services;
    }

}