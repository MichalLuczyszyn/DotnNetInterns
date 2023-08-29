using System.Text;
using System.Web;

namespace Playground.IntegrationTests.Common;


internal static class DictionaryExtensions
{
    public static string ToQueryString(this Dictionary<string, string> dictionary)
    {
        var queryString = string.Join("&", dictionary.Select(a => $"{a.Key}={HttpUtility.UrlEncode(a.Value, Encoding.UTF8)}"));

        return queryString;
    }
}