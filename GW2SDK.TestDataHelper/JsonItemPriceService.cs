using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GuildWars2.Commerce.Prices;

namespace GuildWars2.TestDataHelper;

public class JsonItemPriceService
{
    private readonly HttpClient http;

    public JsonItemPriceService(HttpClient http)
    {
        this.http = http;
    }

    public async Task<ISet<string>> GetJsonItemPrices(IProgress<ICollectionContext> progress)
    {
        var ids = await GetItemPriceIds().ConfigureAwait(false);
        var items = new SortedSet<string>();
        await foreach (var item in GetJsonItemPricesById(ids, progress))
        {
            items.Add(item);
        }

        return items;
    }

    private async Task<IReadOnlyCollection<int>> GetItemPriceIds()
    {
        var request = new ItemPricesIndexRequest();
        var response = await request.SendAsync(http, CancellationToken.None);
        return response.Values;
    }

    public IAsyncEnumerable<string> GetJsonItemPricesById(
        IReadOnlyCollection<int> itemIds,
        IProgress<ICollectionContext>? progress = default,
        CancellationToken cancellationToken = default
    )
    {
        var producer = SplitQuery.Create<int, string>(
            async (range, ct) =>
            {
                var request = new BulkRequest("/v2/commerce/prices") { Ids = range };
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
