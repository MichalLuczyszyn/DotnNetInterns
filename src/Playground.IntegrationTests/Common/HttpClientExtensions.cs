using System.Net.Http.Headers;
using Playground.Application.Security;
using Playground.Core.Globals;
using Playground.Core.Roles;
using Playground.IntegrationTests.Configurations;

namespace Playground.IntegrationTests.Common;

internal static class HttpClientExtensions
{
    public static void AuthorizeForUser(this HttpClient client)
    {
        var scopeFactory = TestEngine.CreateServiceScopeFactory();

        using var scope = scopeFactory.CreateScope();

        var tokenResult = ServiceFactory.GetScopeService<IAuthenticator>(scope).CreateToken(GlobalData.DefaultUserId, SystemRoles.user);

        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenResult.AccessToken);
    }
}