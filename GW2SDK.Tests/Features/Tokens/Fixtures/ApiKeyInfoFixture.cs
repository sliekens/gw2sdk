using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Tests.TestInfrastructure;
using GW2SDK.Tokens.Http;
using Xunit;

namespace GW2SDK.Tests.Features.Tokens.Fixtures
{
    public class ApiKeyInfoFixture : IAsyncLifetime
    {
        public string ApiKeyInfoJson { get; private set; }

        public async Task InitializeAsync()
        {
            await using var container = new Container();
            var http = container.Resolve<HttpClient>();

            ApiKeyInfoJson = await GetTokenInfoJson(http, ConfigurationManager.Instance.ApiKeyFull);
        }

        public Task DisposeAsync() => Task.CompletedTask;

        private async Task<string> GetTokenInfoJson(HttpClient http, string accessToken)
        {
            var request = new TokenInfoRequest(accessToken);
            using var response = await http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
    }
}
