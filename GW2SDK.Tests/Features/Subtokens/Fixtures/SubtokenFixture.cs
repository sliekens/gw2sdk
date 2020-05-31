using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Subtokens.Impl;
using GW2SDK.Tests.TestInfrastructure;
using Xunit;

namespace GW2SDK.Tests.Features.Subtokens.Fixtures
{
    public class SubtokenFixture : IAsyncLifetime
    {
        public string CreatedSubtoken { get; private set; }

        public async Task InitializeAsync()
        {
            await using var container = new Container();
            var http = container.Resolve<IHttpClientFactory>().CreateClient("GW2SDK");

            var request = new CreateSubtokenRequest(ConfigurationManager.Instance.ApiKeyFull);
            using var response = await http.SendAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            CreatedSubtoken = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        }

        public Task DisposeAsync() => Task.CompletedTask;
    }
}
