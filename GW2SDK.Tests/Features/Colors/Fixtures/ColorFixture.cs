using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Infrastructure.Colors;
using GW2SDK.Tests.Shared.Fixtures;
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
            await Db.LoadSnapshot();

            using (var http = new HttpFixture())
            {
                var ids = await GetColorIds(http.Http);

                ids = ids.Except(Db.Index).ToList();

                while (ids.Count != 0)
                {
                    var batch = ids.Take(200).ToList();
                    foreach (var color in await GetColorsByIdRaw(http.Http, batch))
                    {
                        Db.AddColor(color);
                        ids = ids.Except(batch).ToList();
                    }
                }
            }

            await Db.CreateSnapshot();
        }

        public Task DisposeAsync() => Task.CompletedTask;

        private async Task<List<int>> GetColorIds(HttpClient http)
        {
            using (var request = new GetColorIdsRequest())
            using (var response = await http.SendAsync(request))
            using (var responseReader = new StreamReader(await response.Content.ReadAsStreamAsync()))
            using (var jsonReader = new JsonTextReader(responseReader))
            {
                response.EnsureSuccessStatusCode();
                var serializer = new JsonSerializer();
                return serializer.Deserialize<List<int>>(jsonReader);
            }
        }

        private async Task<List<string>> GetColorsByIdRaw(HttpClient http, IReadOnlyList<int> colorIds)
        {
            using (var request = new GetColorsByIdRequest(colorIds))
            using (var response = await http.SendAsync(request))
            using (var responseReader = new StreamReader(await response.Content.ReadAsStreamAsync()))
            using (var jsonReader = new JsonTextReader(responseReader))
            {
                response.EnsureSuccessStatusCode();
                var array = await JToken.ReadFromAsync(jsonReader);
                return array.Children<JObject>().Select(obj => obj.ToString(Formatting.None)).ToList();
            }
        }
    }
}
