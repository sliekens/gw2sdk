using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Commerce.Prices.Http;
using GW2SDK.Http;

namespace GW2SDK.TestDataHelper
{
    public class JsonItemPriceService
    {
        private readonly HttpClient _http;

        public JsonItemPriceService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<string>> GetJsonItemPrices(bool indented)
        {
            // For this data set, I decided to limit the results
            // - because the data is really pretty much the same across all items
            var ids = await GetItemPriceIds().ConfigureAwait(false);
            var list = new List<string>();
            var head = ids.Take(100);
            var body = ids.Skip(100)
                .Where((_, index) => index % 100 == 0);
            var tail = ids.TakeLast(100);
            var dataSet = head.Concat(body).Concat(tail).ToList();
            var tasks = dataSet.Buffer(200).Select(subset => GetJsonItemPricesById(subset.ToList(), indented));
            foreach (var result in await Task.WhenAll(tasks).ConfigureAwait(false))
            {
                list.AddRange(result);
            }

            return list;
        }

        private async Task<List<int>> GetItemPriceIds()
        {
            var request = new ItemPricesIndexRequest();
            using var response = await _http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead)
                .ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using var json = await response.Content.ReadAsJsonAsync().ConfigureAwait(false);
            return json.RootElement.EnumerateArray().Select(item => item.GetInt32()).ToList();
        }

        private async Task<List<string>> GetJsonItemPricesById(IReadOnlyCollection<int> itemIds, bool indented)
        {
            var request = new ItemPricesByIdsRequest(itemIds);
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
