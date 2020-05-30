using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Skins.Impl;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GW2SDK.TestDataHelper
{
    public class JsonSkinService
    {
        private readonly HttpClient _http;

        public JsonSkinService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<string>> GetAllJsonSkins(bool indented)
        {
            var ids = await GetSkinIds();
            var list = new List<string>(ids.Count);
            var tasks = ids.Buffer(200).Select(subset => GetJsonSkinsByIds(subset.ToList(), indented));
            foreach (var result in await Task.WhenAll(tasks))
            {
                list.AddRange(result);
            }

            return list;
        }

        private async Task<List<int>> GetSkinIds()
        {
            using var request = new GetSkinsIndexRequest();
            using var response = await _http.SendAsync(request);
            var json = await response.Content.ReadAsStringAsync();
            response.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<List<int>>(json);
        }

        private async Task<List<string>> GetJsonSkinsByIds(IReadOnlyCollection<int> skinIds, bool indented)
        {
            using var request = new GetSkinsByIdsRequest.Builder(skinIds).GetRequest();
            using var response = await _http.SendAsync(request);
            using var responseReader = new StreamReader(await response.Content.ReadAsStreamAsync());
            using var jsonReader = new JsonTextReader(responseReader);
            response.EnsureSuccessStatusCode();

            // API returns a JSON array but we want a List of JSON objects instead
            var array = await JToken.ReadFromAsync(jsonReader);
            return array.Children<JObject>().Select(skin => skin.ToString(indented ? Formatting.Indented : Formatting.None)).ToList();
        }
    }
}
