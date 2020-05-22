﻿using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Items.Impl;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GW2SDK.TestDataHelper
{
    public class JsonItemService
    {
        private readonly HttpClient _http;

        public JsonItemService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<string>> GetAllJsonItems(bool indented)
        {
            var ids = await GetItemsIndex();
            var list = new List<string>(ids.Count);
            var tasks = ids.Buffer(200).Select(subset => GetJsonItemsByIds(subset.ToList(), indented));
            foreach (var result in await Task.WhenAll(tasks))
            {
                list.AddRange(result);
            }

            return list;
        }

        private async Task<List<int>> GetItemsIndex()
        {
            using var request = new GetItemsIndexRequest();
            using var response = await _http.SendAsync(request);
            var json = await response.Content.ReadAsStringAsync();
            response.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<List<int>>(json);
        }

        private async Task<List<string>> GetJsonItemsByIds(IReadOnlyList<int> itemIds, bool indented)
        {
            using var request = new GetItemsByIdsRequest.Builder(itemIds).GetRequest();
            using var response = await _http.SendAsync(request);
            using var responseReader = new StreamReader(await response.Content.ReadAsStreamAsync());
            using var jsonReader = new JsonTextReader(responseReader);
            response.EnsureSuccessStatusCode();

            // API returns a JSON array but we want a List of JSON objects instead
            var array = await JToken.ReadFromAsync(jsonReader);
            return array.Children<JObject>().Select(item => item.ToString(indented ? Formatting.Indented : Formatting.None)).ToList();
        }
    }
}
