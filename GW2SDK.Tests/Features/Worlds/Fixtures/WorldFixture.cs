using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Infrastructure.Worlds;
using GW2SDK.Tests.Shared;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xunit;

namespace GW2SDK.Tests.Features.Worlds.Fixtures
{
    public class WorldFixture : IAsyncLifetime
    {
        public InMemoryWorldDb Db { get; } = new InMemoryWorldDb();

        public async Task InitializeAsync()
        {
            var http = HttpClientFactory.CreateDefault();

            // Seed InMemoryWorldDb with API data for later use in integration tests
            foreach (var world in await GetAllWorldsRaw(http))
            {
                Db.AddWorld(world);
            }
        }

        public Task DisposeAsync() => Task.CompletedTask;

        private async Task<List<string>> GetAllWorldsRaw(HttpClient http)
        {
            using (var request = new GetAllWorldsRequest())
            using (var response = await http.SendAsync(request))
            using (var responseReader = new StreamReader(await response.Content.ReadAsStreamAsync()))
            using (var jsonReader = new JsonTextReader(responseReader))
            {
                response.EnsureSuccessStatusCode();

                // API returns a JSON array but we want a List of JSON objects instead
                var array = await JToken.ReadFromAsync(jsonReader);
                return array.Children<JObject>().Select(world => world.ToString(Formatting.None)).ToList();
            }
        }
    }
}
