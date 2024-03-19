using Mapster;
using Mapster.Utils;

using MapsterMapper;

using Microsoft.Extensions.DependencyInjection;

using System.Reflection;

namespace Sample_CleanArchitecture_CQRS.Application.Configuration;

internal static class DependencyInjection
{
    public static IServiceCollection AddApplicationMapping(this IServiceCollection services)
    {
        var config = TypeAdapterConfig.GlobalSettings;

        Assembly assembly = Assembly.GetExecutingAssembly();
        config.Scan(assembly);

        // config.ScanInheritedTypes(assembly);

        return services;
    }
}
