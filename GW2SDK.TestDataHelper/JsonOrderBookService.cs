using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GuildWars2.Commerce.Listings;

namespace GuildWars2.TestDataHelper;

public class JsonOrderBookService
{
    private readonly HttpClient http;

    public JsonOrderBookService(HttpClient http)
    {
        this.http = http;
    }

    public async Task<ISet<string>> GetJsonOrderBooks(IProgress<ResultContext> progress)
    {
        var ids = await GetOrderBookIds().ConfigureAwait(false);
        var items = new SortedSet<string>();
        await foreach (var item in GetJsonItemListingsById(ids, progress))
        {
            items.Add(item);
        }

        return items;
    }

    private async Task<HashSet<int>> GetOrderBookIds() =>
        await new OrderBooksIndexRequest().SendAsync(http, CancellationToken.None);

    public IAsyncEnumerable<string> GetJsonItemListingsById(
        IReadOnlyCollection<int> itemIds,
        IProgress<ResultContext>? progress = default,
        CancellationToken cancellationToken = default
    )
    {
        var producer = BulkQuery.Create<int, string>(
            async (chunk, ct) =>
            {
                var request = new BulkRequest("/v2/commerce/listings") { Ids = chunk };
                var json = await request.SendAsync(http, ct);
                return json.Indent(false)
                    .RootElement.EnumerateArray()
                    .Select(
                        item => item.ToString()
                            ?? throw new InvalidOperationException("Unexpected null in JSON array.")
                    )
                    .ToList();
            }
        );
        return producer.QueryAsync(
            itemIds,
            progress: progress,
            cancellationToken: cancellationToken
        );
    }
}
