using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Accounts.Http;
using GW2SDK.Http;
using GW2SDK.Tests.TestInfrastructure;
using Xunit;

namespace GW2SDK.Tests.Features.Accounts
{
    public class AccountFixture : IAsyncLifetime
    {
        public string BasicAccount { get; private set; }

        public string FullAccount { get; private set; }

        public async Task InitializeAsync()
        {
            await using var services = new Composer();
            var http = services.Resolve<HttpClient>();

            BasicAccount = await GetAccountRaw(http, ConfigurationManager.Instance.ApiKeyBasic);

            FullAccount = await GetAccountRaw(http, ConfigurationManager.Instance.ApiKeyFull);
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
