using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Tests.TestInfrastructure;
using GW2SDK.Tokens.Impl;
using Xunit;

namespace GW2SDK.Tests.Features.Tokens.Fixtures
{
    public class ApiKeyInfoFixture : IAsyncLifetime
    {
        public string ApiKeyInfoJson { get; private set; }

        public async Task InitializeAsync()
        {
            var http = new Container().Resolve<IHttpClientFactory>().CreateClient("GW2SDK");

            ApiKeyInfoJson = await GetTokenInfoJson(http, ConfigurationManager.Instance.ApiKeyFull);
        }

        public Task DisposeAsync() => Task.CompletedTask;

        private async Task<string> GetTokenInfoJson(HttpClient http, string accessToken)
        {
            var request = new TokenInfoRequest(accessToken);
            using var response = await http.SendAsync(request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
    }
}
