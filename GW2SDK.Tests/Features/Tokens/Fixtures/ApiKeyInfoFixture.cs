using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Infrastructure.Tokens;
using GW2SDK.Tests.Shared;
using Xunit;

namespace GW2SDK.Tests.Features.Tokens.Fixtures
{
    public class ApiKeyInfoFixture : IAsyncLifetime
    {
        public string ApiKeyInfoJson { get; private set; }

        public async Task InitializeAsync()
        {
            var http = HttpClientFactory.CreateDefault();

            ApiKeyInfoJson = await GetTokenInfoJson(http, ConfigurationManager.Instance.ApiKeyFull);
        }

        public Task DisposeAsync() => Task.CompletedTask;

        private async Task<string> GetTokenInfoJson(HttpClient http, string accessToken)
        {
            using (var request = new GetTokenInfoRequest.Builder(accessToken).GetRequest())
            using (var response = await http.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
        }
    }
}
