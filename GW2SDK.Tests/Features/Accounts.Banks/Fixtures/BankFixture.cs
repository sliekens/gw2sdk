using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Accounts.Banks.Impl;
using GW2SDK.Tests.TestInfrastructure;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xunit;

namespace GW2SDK.Tests.Features.Accounts.Banks.Fixtures
{
    public class BankFixture : IAsyncLifetime
    {
        public string Bank { get; set; }

        public async Task InitializeAsync()
        {
            await using var container = new Container();
            var http = container.Resolve<IHttpClientFactory>().CreateClient("GW2SDK");
            var request = new BankRequest(ConfigurationManager.Instance.ApiKeyFull);
            using var response = await http.SendAsync(request);
            using var responseReader = new StreamReader(await response.Content.ReadAsStreamAsync());
            using var jsonReader = new JsonTextReader(responseReader);
            response.EnsureSuccessStatusCode();

            var array = await JArray.LoadAsync(jsonReader);
            Bank = array.ToString(Formatting.None);
        }

        public Task DisposeAsync() => Task.CompletedTask;
    }
}
