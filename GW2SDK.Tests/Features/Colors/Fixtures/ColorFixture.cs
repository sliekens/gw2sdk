using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Infrastructure.Colors;
using GW2SDK.Tests.Shared;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xunit;

namespace GW2SDK.Tests.Features.Colors.Fixtures
{
    public class ColorFixture : IAsyncLifetime
    {
        public InMemoryColorDb Db { get; } = new InMemoryColorDb();

        public async Task InitializeAsync()
        {
            var http = HttpClientFactory.CreateDefault();

            // Seed InMemoryColorDb with API data for later use in integration tests
            foreach (var color in await GetAllJsonColors(http))
            {
                Db.AddColor(color);
            }
        }

        public Task DisposeAsync() => Task.CompletedTask;

        private async Task<List<string>> GetAllJsonColors(HttpClient http)
        {
            using (var request = new GetAllColorsRequest())
            using (var response = await http.SendAsync(request))
            using (var responseReader = new StreamReader(await response.Content.ReadAsStreamAsync()))
            using (var jsonReader = new JsonTextReader(responseReader))
            {
                response.EnsureSuccessStatusCode();

                // API returns a JSON array but we want a List of JSON objects instead
                var array = await JToken.ReadFromAsync(jsonReader);
                return array.Children<JObject>().Select(obj => obj.ToString(Formatting.None)).ToList();
            }
        }
    }
}
