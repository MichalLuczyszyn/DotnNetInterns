using System.Net;
using System.Text;
using System.Text.Json;
using FluentAssertions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Playground.Application.Auth;
using Playground.Application.DTO;
using Playground.Application.Users.SignUp;
using Playground.Core.Globals;
using Playground.Core.Roles;
using Playground.IntegrationTests.Common;

namespace Playground.IntegrationTests.Tests.Users;

public class SignIn
{
    private readonly HttpClient _httpClient;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public SignIn()
    {
        _serviceScopeFactory = TestEngine.CreateServiceScopeFactory();
        _httpClient = TestEngine.CreateClient();
    }

    [Test]
    public async Task sign_in_should_return_ok_200_status_code_and_jwt_token()
    {
        var scope = _serviceScopeFactory.CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
        
        var signUpCommand =
            new SignUpCommand("testEmail@com.pl", "Test", "Password123)(", "Test User", SystemRoles.user)
            {
                UserId = Guid.NewGuid()
            };

        await mediator.Send(signUpCommand, new CancellationToken());
        
        var requestContent = new StringContent(JsonSerializer.Serialize(new Dictionary<string, string>()
            {
                { "email", "testEmail@com.pl" }, {"password", "Password123)("}
            }),
            Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync($"users/sign-in", requestContent);

        response.Should().HaveStatusCode(HttpStatusCode.OK);
    }
}