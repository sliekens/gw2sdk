using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Subtokens.Http;
using GW2SDK.Tests.TestInfrastructure;
using Xunit;

namespace GW2SDK.Tests.Features.Subtokens.Fixtures
{
    public class SubtokenFixture : IAsyncLifetime
    {
        public string CreatedSubtoken { get; private set; }

        public async Task InitializeAsync()
        {
            await using var services = new Composer();
            var http = services.Resolve<HttpClient>();

            var request = new CreateSubtokenRequest(ConfigurationManager.Instance.ApiKeyFull);
            using var response = await http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();
            CreatedSubtoken = await response.Content.ReadAsStringAsync();
        }

        public Task DisposeAsync() => Task.CompletedTask;
    }
}
