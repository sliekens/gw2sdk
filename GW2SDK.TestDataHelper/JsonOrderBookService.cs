using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Commerce.Listings.Http;
using GW2SDK.Http;

namespace GW2SDK.TestDataHelper
{
    public class JsonOrderBookService
    {
        private readonly HttpClient http;

        public JsonOrderBookService(HttpClient http)
        {
            this.http = http;
        }

        public async Task<ISet<string>> GetJsonOrderBooks()
        {
            var ids = await GetOrderBookIds()
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

                work.Add(GetJsonItemListingsById(batch.ToList()));
            }

            while (work.Count > 0)
            {
                var done = await Task.WhenAny(work);
                result.AddRange(done.Result);

                work.Remove(done);

                if (batches.TryDequeue(out var batch))
                {
                    work.Add(GetJsonItemListingsById(batch.ToList()));
                }
            }

            return new SortedSet<string>(result, StringComparer.Ordinal);
        }

        private async Task<List<int>> GetOrderBookIds()
        {
            var request = new OrderBooksIndexRequest();
            using var response = await http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead)
                .ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using var json = await response.Content.ReadAsJsonAsync()
                .ConfigureAwait(false);
            return json.RootElement.EnumerateArray()
                .Select(item => item.GetInt32())
                .ToList();
        }

        private async Task<List<string>> GetJsonItemListingsById(IReadOnlyCollection<int> itemIds)
        {
            var request = new OrderBooksByIdsRequest(itemIds);
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
