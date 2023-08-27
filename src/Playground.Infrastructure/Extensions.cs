using System.Reflection;
using System.Runtime.CompilerServices;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Playground.Application.Clock;
using Playground.Core.Repositories;
using Playground.Infrastructure.Auth;
using Playground.Infrastructure.Contexts;
using Playground.Infrastructure.Exceptions;
using Playground.Infrastructure.Repositories;
using Playground.Infrastructure.Security;
using Playground.Infrastructure.Seeders;

[assembly: InternalsVisibleTo("Playground.Integration.Tests")]

namespace Playground.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();
        services.Configure<AppOptions.AppOptions>(configuration.GetRequiredSection("app"));
        services.AddSingleton<ExceptionMiddleware>();
        services.AddHttpContextAccessor();
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

        services.AddSingleton<IClock, Clock.Clock>();
        services.AddScoped<IUserRepository, UserRepository>();
        
        var options = GetOptions<PostgresOptions>(configuration, "database");
        
        services.AddDbContext<WriteDbContext>(x => x.UseNpgsql(options.ConnectionString));
        services.AddDbContext<ReadDbContext>(x => x.UseNpgsql(options.ConnectionString));
        services.AddHostedService<DatabaseInitializer>();
        services.AddSecurity();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerConfig();

        services.AddAuth(configuration);

        return services;
    }

    public static WebApplication UseInfrastructure(this WebApplication app)
    {
        app.UseMiddleware<ExceptionMiddleware>();
        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();

        return app;
    }

    public static T GetOptions<T>(this IConfiguration configuration, string sectionName) where T : class, new()
    {
        var options = new T();
        var section = configuration.GetRequiredSection(sectionName);
        section.Bind(options);

        return options;
    }
    
    public class PostgresOptions
    {
        public string ConnectionString { get; set; }
    }
}