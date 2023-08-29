using Microsoft.Extensions.DependencyInjection;
using Playground.Infrastructure.Contexts;

namespace Playground.IntegrationTests.Common;

internal static class ServiceFactory
{
    public static WriteDbContext GetScopeDbContext(IServiceScope scope) => GetScopeService<WriteDbContext>(scope);

    public static T GetScopeService<T>(IServiceScope scope)
    {
        var service = scope.ServiceProvider.GetRequiredService<T>();

        return service;
    }
}