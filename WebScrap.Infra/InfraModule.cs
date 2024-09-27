using Microsoft.Extensions.DependencyInjection;
using WebScrap.Infra.Persistence;
using WebScrap.Application.Abstractions;

namespace WebScrap.Infra;

public static class InfraModule
{
    public static IServiceCollection AddInfra(this IServiceCollection services)
    {
        services.AddScoped<ISqlConnectionFactory, SqlConnectionFactory>();

        return services;
    }
}

