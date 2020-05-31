using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Accounts.Impl;
using GW2SDK.Tests.TestInfrastructure;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xunit;

namespace GW2SDK.Tests.Features.Accounts.Fixtures
{
    public class AccountFixture : IAsyncLifetime
    {
        public InMemoryAccountDb Db { get; } = new InMemoryAccountDb();

        public async Task InitializeAsync()
        {
            await using var container = new Container();
            var http = container.Resolve<IHttpClientFactory>().CreateClient("GW2SDK");

            var basic = await GetAccountRaw(http, ConfigurationManager.Instance.ApiKeyBasic);
            Db.SetBasicAccount(basic);

            var full = await GetAccountRaw(http, ConfigurationManager.Instance.ApiKeyFull);
            Db.SetFullAccount(full);
        }

        public Task DisposeAsync() => Task.CompletedTask;

        private async Task<string> GetAccountRaw(HttpClient http, string accessToken)
        {
            var request = new AccountRequest(accessToken);
            using var response = await http.SendAsync(request).ConfigureAwait(false);
            using var responseReader = new StreamReader(await response.Content.ReadAsStreamAsync());
            using var jsonReader = new JsonTextReader(responseReader);
            response.EnsureSuccessStatusCode();
            var obj = await JToken.ReadFromAsync(jsonReader);
            return obj.ToString(Formatting.None);
        }
    }
}
