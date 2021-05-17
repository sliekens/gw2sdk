using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Commerce.Listings.Http;
using GW2SDK.Http;

namespace GW2SDK.TestDataHelper
{
    public class JsonItemListingService
    {
        private readonly HttpClient _http;

        public JsonItemListingService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<string>> GetAllJsonItemListing(bool indented)
        {
            var ids = await GetItemListingIds()
                .ConfigureAwait(false);
            var list = new List<string>(ids.Count);
            var tasks = ids.Buffer(200)
                .Select(subset => GetJsonItemListingsById(subset.ToList(), indented));
            foreach (var result in await Task.WhenAll(tasks)
                .ConfigureAwait(false))
            {
                list.AddRange(result);
            }

            return list;
        }

        private async Task<List<int>> GetItemListingIds()
        {
            var request = new ItemListingsIndexRequest();
            using var response = await _http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead)
                .ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using var json = await response.Content.ReadAsJsonAsync()
                .ConfigureAwait(false);
            return json.RootElement.EnumerateArray()
                .Select(item => item.GetInt32())
                .ToList();
        }

        private async Task<List<string>> GetJsonItemListingsById(IReadOnlyCollection<int> itemIds, bool indented)
        {
            var request = new ItemListingsByIdsRequest(itemIds);
            using var response = await _http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead)
                .ConfigureAwait(false);
            response.EnsureSuccessStatusCode();

            // API returns a JSON array but we want a List of JSON objects instead
            using var json = await response.Content.ReadAsJsonAsync()
                .ConfigureAwait(false);
            return json.Indent(indented)
                .RootElement.EnumerateArray()
                .Select(item =>
                    item.ToString() ?? throw new InvalidOperationException("Unexpected null in JSON array."))
                .ToList();
        }
    }
}
