using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Http;
using GW2SDK.Skins.Http;

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
            var ids = await GetSkinIds().ConfigureAwait(false);
            var list = new List<string>(ids.Count);
            var tasks = ids.Buffer(200).Select(subset => GetJsonSkinsByIds(subset.ToList(), indented));
            foreach (var result in await Task.WhenAll(tasks).ConfigureAwait(false))
            {
                list.AddRange(result);
            }

            return list;
        }

        private async Task<List<int>> GetSkinIds()
        {
            var request = new SkinsIndexRequest();
            using var response = await _http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead)
                .ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using var json = await response.Content.ReadAsJsonAsync().ConfigureAwait(false);
            return json.RootElement.EnumerateArray().Select(item => item.GetInt32()).ToList();
        }

        private async Task<List<string>> GetJsonSkinsByIds(IReadOnlyCollection<int> skinIds, bool indented)
        {
            var request = new SkinsByIdsRequest(skinIds);
            using var response = await _http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead)
                .ConfigureAwait(false);
            response.EnsureSuccessStatusCode();

            // API returns a JSON array but we want a List of JSON objects instead
            using var json = await response.Content.ReadAsJsonAsync().ConfigureAwait(false);
            return json.Indent(indented)
                .RootElement.EnumerateArray()
                .Select(item =>
                    item.ToString() ?? throw new InvalidOperationException("Unexpected null in JSON array."))
                .ToList();
        }
    }
}
