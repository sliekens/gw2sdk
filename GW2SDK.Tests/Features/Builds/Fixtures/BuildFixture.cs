using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Extensions;
using GW2SDK.Features.Builds.Infrastructure;
using GW2SDK.Tests.Shared.Fixtures;
using Xunit;

namespace GW2SDK.Tests.Features.Builds.Fixtures
{
    public class BuildFixture : IAsyncLifetime
    {
        public string JsonBuild { get; set; }

        public async Task InitializeAsync()
        {
            var configuration = new ConfigurationFixture();
            var service = new JsonBuildService(new HttpClient().WithBaseAddress(configuration.BaseAddress));

            JsonBuild = await service.GetBuild();
        }

        public Task DisposeAsync() => Task.CompletedTask;
    }
}
