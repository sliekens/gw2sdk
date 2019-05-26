using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Extensions;
using GW2SDK.Infrastructure.Accounts;
using GW2SDK.Tests.Shared;
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
            var http = HttpClientFactory.CreateDefault();

            http.UseAccessToken(ConfigurationManager.Instance.ApiKeyBasic);
            var basic = await GetAccountRaw(http);
            Db.SetBasicAccount(basic);

            http.UseAccessToken(ConfigurationManager.Instance.ApiKeyFull);
            var full = await GetAccountRaw(http);
            Db.SetFullAccount(full);
        }

        public Task DisposeAsync() => Task.CompletedTask;

        private async Task<string> GetAccountRaw(HttpClient http)
        {
            using (var request = new GetAccountRequest())
            using (var response = await http.SendAsync(request))
            using (var responseReader = new StreamReader(await response.Content.ReadAsStreamAsync()))
            using (var jsonReader = new JsonTextReader(responseReader))
            {
                response.EnsureSuccessStatusCode();
                var obj = await JToken.ReadFromAsync(jsonReader);
                return obj.ToString(Formatting.None);
            }
        }
    }
}
