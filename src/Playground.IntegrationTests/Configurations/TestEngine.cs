using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.TestHost;
using System.Diagnostics;
using NUnit.Framework;
using Playground.Infrastructure.Contexts;
using Playground.IntegrationTests.Common;
using Playground.IntegrationTests.Configurations;

namespace Playground.IntegrationTests;

[SetUpFixture]
internal class TestEngine
{
    private static APIClientFactory SystemUnderTest;

    internal static HttpClient CreateClient()
    {
        var httpClient = SystemUnderTest.CreateClient();
        httpClient.AuthorizeForUser();

        return httpClient;
    }

    internal static HttpClient CreateClient(Action<IServiceCollection> mockServices)
    {
        var httpClient = SystemUnderTest
            .WithWebHostBuilder(cfg => cfg.ConfigureTestServices(mockServices)).CreateClient();

        httpClient.AuthorizeForUser();
        return httpClient;
    }

    internal static IServiceScopeFactory CreateServiceScopeFactory() =>
        SystemUnderTest.Services.GetRequiredService<IServiceScopeFactory>();


    [OneTimeSetUp]
    public static async Task RunBeforeTests()
    {
        // wymaga pobrania i uruchomienia dockera
        await DatabaseContainer.StartAsync();

        await SetupSystemUnderTest();
    }

    [OneTimeTearDown]
    public static async Task RunAfterTests()
    {
        await DatabaseContainer.Stop();
    }


    private static async Task SetupSystemUnderTest()
    {
        var connectionString = DatabaseContainer.GetConnectionString();

        SystemUnderTest = new APIClientFactory(connectionString);

        await EnsureDatabaseCreated();
    }

    private static async Task EnsureDatabaseCreated()
    {
        var scopeFactory = CreateServiceScopeFactory();

        using (var scope = scopeFactory.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<WriteDbContext>();

            // dla pewności że korzystamy z bazy danych z kontenera
            if (db.Database.GetConnectionString() != DatabaseContainer.GetConnectionString())
                throw new InvalidOperationException();

            await db.Database.MigrateAsync();
        }
    }
}