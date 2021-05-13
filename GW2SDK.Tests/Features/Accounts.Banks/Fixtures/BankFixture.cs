using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Accounts.Banks.Http;
using GW2SDK.Http;
using GW2SDK.Tests.TestInfrastructure;
using Xunit;

namespace GW2SDK.Tests.Features.Accounts.Banks.Fixtures
{
    public class BankFixture : IAsyncLifetime
    {
        public string Bank { get; set; }

        public async Task InitializeAsync()
        {
            await using var container = new Container();
            var http = container.Resolve<HttpClient>();
            var request = new BankRequest(ConfigurationManager.Instance.ApiKeyFull);
            using var response = await http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();
            using var json = await response.Content.ReadAsJsonAsync();
            Bank = json.Indent(false).RootElement.ToString();
        }

        public Task DisposeAsync() => Task.CompletedTask;
    }
}
