using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Playground.Application.Security;
using Playground.Core.Entities.Users;

namespace Playground.Infrastructure.Security;

internal static class Extensions
{
    public static IServiceCollection AddSecurity(this IServiceCollection services)
    {
        services
            .AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>()
            .AddSingleton<IPasswordManager, PasswordManager>();

        return services;
    }
}