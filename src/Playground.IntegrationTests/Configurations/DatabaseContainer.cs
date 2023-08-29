using DotNet.Testcontainers.Builders;
using Testcontainers.PostgreSql;

namespace Playground.IntegrationTests.Configurations;

internal static class DatabaseContainer
{
    private static readonly PostgreSqlContainer _testDatabaseContainer =
        new PostgreSqlBuilder()
            .WithWaitStrategy(Wait.ForUnixContainer())
            .Build();

    internal static string GetConnectionString() => _testDatabaseContainer.GetConnectionString();


    internal static Task StartAsync() => _testDatabaseContainer.StartAsync();
    internal static Task Stop() => _testDatabaseContainer.DisposeAsync().AsTask();
}