using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Builds.Impl;
using GW2SDK.Tests.TestInfrastructure;
using Xunit;

namespace GW2SDK.Tests.Features.Builds.Fixtures
{
    public class BuildFixture : IAsyncLifetime
    {
        public string Build { get; private set; }

        public async Task InitializeAsync()
        {
            await using var container = new Container();
            var http = container.Resolve<IHttpClientFactory>().CreateClient("GW2SDK");

            var request = new BuildRequest();
            using var response = await http.SendAsync(request);
            response.EnsureSuccessStatusCode();
            Build = await response.Content.ReadAsStringAsync();
        }

        public Task DisposeAsync() => Task.CompletedTask;
    }
}
