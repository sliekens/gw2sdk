using System.Threading.Tasks;
using GW2SDK.Infrastructure.Builds;
using GW2SDK.Tests.Shared.Fixtures;
using Xunit;

namespace GW2SDK.Tests.Features.Builds.Fixtures
{
    public class BuildFixture : IAsyncLifetime
    {
        public string JsonBuildObject { get; set; }

        public async Task InitializeAsync()
        {
            var http = new HttpFixture();

            var service = new BuildJsonService(http.Http);

            var response = await service.GetBuild();
            response.EnsureSuccessStatusCode();
            JsonBuildObject = await response.Content.ReadAsStringAsync();
        }

        public Task DisposeAsync() => Task.CompletedTask;
    }
}
