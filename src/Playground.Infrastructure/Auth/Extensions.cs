using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Playground.Application.Security;

namespace Playground.Infrastructure.Auth;

internal static class Extensions
{
    private const string OptionsSectionName = "auth";
    
    public static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration configuration)
    {
        var options = configuration.GetOptions<AuthOptions>(OptionsSectionName);

        services
            .Configure<AuthOptions>(configuration.GetRequiredSection(OptionsSectionName))
            .AddSingleton<IAuthenticator, Authenticator>()
            .AddSingleton<ITokenStorage, HttpContextTokenStorage>()
            .AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddCookie(cfg => cfg.SlidingExpiration = true)
            .AddJwtBearer(o =>
            {
                o.Audience = options.Audience;
                o.IncludeErrorDetails = true;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = options.Issuer,
                    ClockSkew = TimeSpan.Zero,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.SigningKey))
                };
                o.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        if (context.Request.Cookies.TryGetValue("token", out var token))
                        {
                            context.Token = token;
                        }

                        return Task.CompletedTask;
                    },
                };
            });

        services.AddAuthorization(authorization =>
        {
            authorization.AddPolicy("is-admin", policy =>
            {
                policy.RequireRole("admin");
            });
        });

        return services;
    }
}