using System.Net;
using System.Text;
using System.Text.Json;
using FluentAssertions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Playground.Application.Users.SignUp;
using Playground.Core.Roles;

namespace Playground.IntegrationTests.Tests.Users;

public class SignUp
{
    private readonly HttpClient _httpClient;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public SignUp()
    {
        _serviceScopeFactory = TestEngine.CreateServiceScopeFactory();
        _httpClient = TestEngine.CreateClient();
    }

    [Test]
    public async Task sign_up_should_return_ok_201()
    {
        var requestContent = new StringContent(JsonSerializer.Serialize(new Dictionary<string, string>()
            {
                { "email", "testEmail@com.pl" }, {"userName", "TestowyUser"}, {"password", "Password123)("},{"fullName", "Full name"},{"role", "user"},
            }),
            Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync($"users/sign-up", requestContent);

        response.Should().HaveStatusCode(HttpStatusCode.Created);
    }
}