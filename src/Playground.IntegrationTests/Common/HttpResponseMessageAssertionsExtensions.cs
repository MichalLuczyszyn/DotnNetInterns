namespace Playground.IntegrationTests.Common;

internal static class HttpResponseMessageAssertionsExtensions
{
    public static Task<T> ParseAndVerify<T>(this HttpResponseMessage instance) where T : class
    {
        var assertion = new HttpResponseMessageAssertions(instance);

        return assertion.ParseAndVerify<T>();
    }
}