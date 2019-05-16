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

            var http = new HttpClient()
                .WithBaseAddress(configuration.BaseAddress)
                .WithLatestSchemaVersion();

            var service = new JsonBuildService(http);

            JsonBuild = await service.GetBuild();
        }

        public Task DisposeAsync() => Task.CompletedTask;
    }
}
