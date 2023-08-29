using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;

namespace Playground.IntegrationTests.Configurations;

public class APIClientFactory : WebApplicationFactory<Program>
{
    private readonly Dictionary<string, string> _configuration;

    public APIClientFactory(string connectionString)
    {
        _configuration = new Dictionary<string, string>
        {
            {"database:connectionString", connectionString}
        };
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(_configuration!)
            .Build();
        
        builder.UseConfiguration(configuration);
    }
}