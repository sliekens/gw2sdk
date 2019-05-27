using System.Threading.Tasks;
using GW2SDK.Infrastructure.Builds;
using GW2SDK.Tests.Shared;
using Xunit;

namespace GW2SDK.Tests.Features.Builds.Fixtures
{
    public class BuildFixture : IAsyncLifetime
    {
        public string Build { get; private set; }

        public async Task InitializeAsync()
        {
            var http = HttpClientFactory.CreateDefault();

            using (var request = new GetBuildRequest())
            using (var response = await http.SendAsync(request).ConfigureAwait(false))
            {
                response.EnsureSuccessStatusCode();
                Build = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            }
        }

        public Task DisposeAsync() => Task.CompletedTask;
    }
}
