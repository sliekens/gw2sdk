using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using GW2SDK.Accounts.Impl;
using GW2SDK.Tests.TestInfrastructure;
using Xunit;

namespace GW2SDK.Tests.Features.Accounts.Fixtures
{
    public class AccountFixture : IAsyncLifetime
    {
        public JsonDocument BasicAccount { get; private set; }

        public JsonDocument FullAccount { get; private set; }

        public async Task InitializeAsync()
        {
            await using var container = new Container();
            var http = container.Resolve<IHttpClientFactory>().CreateClient("GW2SDK");

            BasicAccount = await GetAccountRaw(http, ConfigurationManager.Instance.ApiKeyBasic);

            FullAccount = await GetAccountRaw(http, ConfigurationManager.Instance.ApiKeyFull);
        }

        public Task DisposeAsync()
        {
            BasicAccount.Dispose();
            FullAccount.Dispose();
            return Task.CompletedTask;
        }

        private async Task<JsonDocument> GetAccountRaw(HttpClient http, string accessToken)
        {
            using var response = await http.SendAsync(new AccountRequest(accessToken));
            response.EnsureSuccessStatusCode();
            await using var json = await response.Content.ReadAsStreamAsync();
            return await JsonDocument.ParseAsync(json);
        }
    }
}
