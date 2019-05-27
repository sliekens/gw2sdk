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

            var ids = await GetWorldIds(http);

            ids = ids.Except(Db.Index).ToList();

            while (ids.Count != 0)
            {
                var batch = ids.Take(200).ToList();
                foreach (var world in await GetWorldsByIdRaw(http, batch))
                {
                    Db.AddWorld(world);
                    ids = ids.Except(batch).ToList();
                }
            }
        }

        public Task DisposeAsync() => Task.CompletedTask;

        private async Task<List<int>> GetWorldIds(HttpClient http)
        {
            using (var request = new GetWorldIdsRequest())
            using (var response = await http.SendAsync(request))
            using (var responseReader = new StreamReader(await response.Content.ReadAsStreamAsync()))
            using (var jsonReader = new JsonTextReader(responseReader))
            {
                response.EnsureSuccessStatusCode();
                var serializer = new JsonSerializer();
                return serializer.Deserialize<List<int>>(jsonReader);
            }
        }

        private async Task<List<string>> GetWorldsByIdRaw(HttpClient http, IReadOnlyList<int> worldIds)
        {
            using (var request = new GetWorldsByIdRequest.Builder(worldIds).GetRequest())
            using (var response = await http.SendAsync(request))
            using (var responseReader = new StreamReader(await response.Content.ReadAsStreamAsync()))
            using (var jsonReader = new JsonTextReader(responseReader))
            {
                response.EnsureSuccessStatusCode();
                var array = await JToken.ReadFromAsync(jsonReader);
                return array.Children<JObject>().Select(world => world.ToString(Formatting.None)).ToList();
            }
        }
    }
}
