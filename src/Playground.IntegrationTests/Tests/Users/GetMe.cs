using System.Net;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Playground.Application.DTO;
using Playground.Application.Users.SignUp;
using Playground.Core.Globals;
using Playground.Core.Roles;
using Playground.IntegrationTests.Common;
using Playground.IntegrationTests.Configurations;

namespace Playground.IntegrationTests.Tests.Users;


internal class GetMe
{
    private readonly HttpClient _httpClient;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public GetMe()
    {
        _serviceScopeFactory = TestEngine.CreateServiceScopeFactory();
        _httpClient = TestEngine.CreateClient();
    }

    [Test]
    public async Task get_users_me_should_return_ok_200_status_code_and_user()
    {
        var response = await _httpClient.GetAsync($"users/me");

        response.Should().HaveStatusCode(HttpStatusCode.OK);

        await ShouldBeValidResponse(response);
    }

    private async Task ShouldBeValidResponse(HttpResponseMessage msg)
    {
        var res = await msg.ParseAndVerify<UserDto>();

        res.Username.Should().Be("Init");
        res.FullName.Should().Be("Default User");
        res.Id.Should().Be(GlobalData.DefaultUserId);
    }
}