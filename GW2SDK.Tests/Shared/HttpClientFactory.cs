using System.Net.Http;
using GW2SDK.Extensions;
using Microsoft.Extensions.Http;

namespace GW2SDK.Tests.Shared
{
    public static class HttpClientFactory
    {
        public static HttpClient CreateDefault()
        {
            var socketsHandler = new SocketsHttpHandler();
            var policyHttpMessageHandler = new PolicyHttpMessageHandler(HttpPolicy.SelectPolicy) { InnerHandler = socketsHandler };

            var http = new HttpClient(policyHttpMessageHandler, true);
            http.UseBaseAddress(ConfigurationManager.Instance.BaseAddress);
            http.UseLatestSchemaVersion();
            return http;
        }
    }
}
