using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Mapster;

using Microsoft.Extensions.DependencyInjection;

namespace Sample_CleanArchitecture_CQRS.Infrastructure.Configuration;
internal static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureMapping(this IServiceCollection services)
    {
        var config = TypeAdapterConfig.GlobalSettings;
        config.Scan(Assembly.GetExecutingAssembly());

        return services;
    }
}

