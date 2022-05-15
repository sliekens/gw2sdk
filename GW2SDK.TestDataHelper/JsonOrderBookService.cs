using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Commerce.Listings;
using GW2SDK.Commerce.Prices;

namespace GW2SDK.TestDataHelper;

public class JsonOrderBookService
{
    private readonly HttpClient http;

    public JsonOrderBookService(HttpClient http)
    {
        this.http = http;
    }

    public async Task<ISet<string>> GetJsonOrderBooks(IProgress<ICollectionContext> progress)
    {
        var ids = await GetOrderBookIds().ConfigureAwait(false);
        var items = new SortedSet<string>();
        await foreach (var item in GetJsonItemListingsById(ids, progress))
        {
            items.Add(item);
        }

        return items;
    }

    private async Task<IReadOnlyCollection<int>> GetOrderBookIds()
    {
        var request = new OrderBooksIndexRequest();
        var response = await request.SendAsync(http, CancellationToken.None);
        return response.Values;
    }

    public IAsyncEnumerable<string> GetJsonItemListingsById(
        IReadOnlyCollection<int> itemIds,
        IProgress<ICollectionContext>? progress = default,
        CancellationToken cancellationToken = default
    )
    {
        var producer = SplitQuery.Create<int, string>(
            async (range, ct) =>
            {
                var request = new BulkRequest("/v2/commerce/listings") { Ids = range };
                var json = await request.SendAsync(http, ct);
                return json.Indent(false)
                    .RootElement.EnumerateArray()
                    .Select(
                        item => item.ToString()
                            ?? throw new InvalidOperationException("Unexpected null in JSON array.")
                    )
                    .ToList();
            },
            progress
        );
        return producer.QueryAsync(itemIds, cancellationToken: cancellationToken);
    }
}
