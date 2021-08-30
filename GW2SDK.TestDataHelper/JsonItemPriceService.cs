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
        private readonly HttpClient http;

        public JsonItemPriceService(HttpClient http)
        {
            this.http = http;
        }

        public async Task<ISet<string>> GetJsonItemPrices()
        {
            var ids = await GetItemPriceIds()
                .ConfigureAwait(false);

            var batches = new Queue<IEnumerable<int>>(ids.Buffer(200));

            var result = new List<string>();
            var work = new List<Task<List<string>>>();

            for (var i = 0; i < 12; i++)
            {
                if (!batches.TryDequeue(out var batch))
                {
                    break;
                }

                work.Add(GetJsonItemPricesById(batch.ToList()));
            }

            while (work.Count > 0)
            {
                var done = await Task.WhenAny(work);
                result.AddRange(done.Result);

                work.Remove(done);

                if (batches.TryDequeue(out var batch))
                {
                    work.Add(GetJsonItemPricesById(batch.ToList()));
                }
            }

            return new SortedSet<string>(result, StringComparer.Ordinal);
        }


        private async Task<List<int>> GetItemPriceIds()
        {
            var request = new ItemPricesIndexRequest();
            using var response = await http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead)
                .ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using var json = await response.Content.ReadAsJsonAsync()
                .ConfigureAwait(false);
            return json.RootElement.EnumerateArray()
                .Select(item => item.GetInt32())
                .ToList();
        }

        private async Task<List<string>> GetJsonItemPricesById(IReadOnlyCollection<int> itemIds)
        {
            var request = new ItemPricesByIdsRequest(itemIds);
            using var response = await http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead)
                .ConfigureAwait(false);
            response.EnsureSuccessStatusCode();

            // API returns a JSON array but we want a List of JSON objects instead
            using var json = await response.Content.ReadAsJsonAsync()
                .ConfigureAwait(false);
            return json.Indent(false)
                .RootElement.EnumerateArray()
                .Select(item =>
                    item.ToString() ?? throw new InvalidOperationException("Unexpected null in JSON array."))
                .ToList();
        }
    }
}
