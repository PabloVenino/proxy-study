using Microsoft.Extensions.DependencyInjection;
using WebScrap.Application.Abstractions;
using WebScrap.Infra.Persistence;

namespace WebScrap.Infra;

public static class InfraModule
{
    public static IServiceCollection AddInfra(this IServiceCollection services)
    {
        services.AddScoped<ISqlConnectionFactory, SqlConnectionFactory>();

        return services;
    }
}

