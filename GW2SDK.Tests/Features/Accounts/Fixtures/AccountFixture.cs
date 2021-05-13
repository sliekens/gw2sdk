using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Accounts.Http;
using GW2SDK.Http;
using GW2SDK.Tests.TestInfrastructure;
using Xunit;

namespace GW2SDK.Tests.Features.Accounts.Fixtures
{
    public class AccountFixture : IAsyncLifetime
    {
        public InMemoryAccountDb Db { get; } = new InMemoryAccountDb();

        public async Task InitializeAsync()
        {
            await using var container = new Container();
            var http = container.Resolve<HttpClient>();

            var basic = await GetAccountRaw(http, ConfigurationManager.Instance.ApiKeyBasic);
            Db.SetBasicAccount(basic);

            var full = await GetAccountRaw(http, ConfigurationManager.Instance.ApiKeyFull);
            Db.SetFullAccount(full);
        }

        public Task DisposeAsync() => Task.CompletedTask;

        private async Task<string> GetAccountRaw(HttpClient http, string accessToken)
        {
            var request = new AccountRequest(accessToken);
            using var response = await http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();
            using var json = await response.Content.ReadAsJsonAsync();
            return json.Indent(false).RootElement.ToString();
        }
    }
}
