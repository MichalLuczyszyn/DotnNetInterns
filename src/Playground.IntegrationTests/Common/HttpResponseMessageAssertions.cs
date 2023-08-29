using System.Net.Http.Json;
using FluentAssertions;
using FluentAssertions.Primitives;

namespace Playground.IntegrationTests.Common;

internal sealed class HttpResponseMessageAssertions : ReferenceTypeAssertions<HttpResponseMessage, HttpResponseMessageAssertions>
{
    public HttpResponseMessageAssertions(HttpResponseMessage instance)
        : base(instance)
    {
    }

    protected override string Identifier => nameof(HttpResponseMessage);

    public async Task<T> ParseAndVerify<T>() where T : class
    {
        var result = await Subject.Content.ReadFromJsonAsync<T>();

        result.Should().NotBeNull();

        return result;
    }
}