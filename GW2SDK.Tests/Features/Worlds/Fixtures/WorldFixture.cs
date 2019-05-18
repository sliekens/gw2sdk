using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Extensions;
using GW2SDK.Features.Common;
using GW2SDK.Features.Worlds.Infrastructure;
using GW2SDK.Tests.Shared.Fixtures;
using Xunit;

namespace GW2SDK.Tests.Features.Worlds.Fixtures
{
    public class WorldFixture : IAsyncLifetime
    {
        public string JsonArrayOfWorlds { get; private set; }

        public ListMetaData ListMetaData { get; private set; }

        public async Task InitializeAsync()
        {
            var configuration = new ConfigurationFixture();

            var http = new HttpClient()
                .WithBaseAddress(configuration.BaseAddress)
                .WithLatestSchemaVersion();

            var service = new JsonWorldService(http);

            var (json, metaData) = await service.GetAllWorlds();
            JsonArrayOfWorlds = json;
            ListMetaData = metaData;
        }

        public Task DisposeAsync() => Task.CompletedTask;
    }
}
