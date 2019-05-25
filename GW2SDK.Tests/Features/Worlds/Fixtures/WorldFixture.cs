using System.Threading.Tasks;
using GW2SDK.Extensions;
using GW2SDK.Features.Common;
using GW2SDK.Infrastructure.Worlds;
using GW2SDK.Tests.Shared.Fixtures;
using Xunit;

namespace GW2SDK.Tests.Features.Worlds.Fixtures
{
    public class WorldFixture : IAsyncLifetime
    {
        public string JsonArrayOfWorlds { get; private set; }

        public IListContext ListContext { get; private set; }

        public async Task InitializeAsync()
        {
            var http = new HttpFixture();

            var service = new WorldJsonService(http.Http);

            var response = await service.GetAllWorlds();
            response.EnsureSuccessStatusCode();
            JsonArrayOfWorlds = await response.Content.ReadAsStringAsync();
            ListContext = response.Headers.GetListContext();
        }

        public Task DisposeAsync() => Task.CompletedTask;
    }
}
