using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Infrastructure.Subtokens;
using GW2SDK.Tests.Shared;
using Xunit;

namespace GW2SDK.Tests.Features.Subtokens.Fixtures
{
    public class SubtokenFixture : IAsyncLifetime
    {
        public string CreatedSubtoken { get; private set; }

        public async Task InitializeAsync()
        {
            var http = new Container().Resolve<IHttpClientFactory>().CreateClient("GW2SDK");

            using (var request = new CreateSubtokenRequest.Builder(ConfigurationManager.Instance.ApiKeyFull).GetRequest())
            using (var response = await http.SendAsync(request).ConfigureAwait(false))
            {
                response.EnsureSuccessStatusCode();
                CreatedSubtoken = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            }
        }

        public Task DisposeAsync() => Task.CompletedTask;
    }
}
