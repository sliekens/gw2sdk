using System.IO;
using System.Threading.Tasks;
using GW2SDK.Infrastructure.Worlds;
using GW2SDK.Tests.Shared.Fixtures;
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
            using (var http = new HttpFixture())
            using (var request = new GetAllWorldsRequest())
            using (var response = await http.Http.SendAsync(request))
            using (var reader = new StreamReader(await response.Content.ReadAsStreamAsync()))
            using (var jsonReader = new JsonTextReader(reader))
            {
                response.EnsureSuccessStatusCode();
                var array = await JToken.ReadFromAsync(jsonReader);
                foreach (var world in array.Children<JObject>()) Db.Worlds.Add(world.ToString(Formatting.None));
            }
        }

        public Task DisposeAsync() => Task.CompletedTask;
    }
}
