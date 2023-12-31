﻿using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using MediatR;

namespace Playground.Application;

public static class Extensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg=>cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));   

        return services;
    }
}