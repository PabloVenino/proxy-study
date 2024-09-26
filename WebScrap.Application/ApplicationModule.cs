using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace WebScrap.Application;

public static class InfraModule
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(config => config.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

        return services;
    }
}