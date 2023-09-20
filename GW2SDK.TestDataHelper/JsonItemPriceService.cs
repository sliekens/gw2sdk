using GuildWars2.Commerce.Prices;

namespace GuildWars2.TestDataHelper;

public class JsonItemPriceService
{
    private readonly HttpClient http;

    public JsonItemPriceService(HttpClient http)
    {
        this.http = http;
    }

    public async Task<ISet<string>> GetJsonItemPrices(IProgress<ResultContext> progress)
    {
        var ids = await GetItemPriceIds().ConfigureAwait(false);
        var items = new SortedSet<string>();
        await foreach (var item in GetJsonItemPricesById(ids, progress))
        {
            items.Add(item);
        }

        return items;
    }

    private async Task<HashSet<int>> GetItemPriceIds() =>
        await new ItemPricesIndexRequest().SendAsync(http, CancellationToken.None);

    public IAsyncEnumerable<string> GetJsonItemPricesById(
        IReadOnlyCollection<int> ids,
        IProgress<ResultContext>? progress = default,
        CancellationToken cancellationToken = default
    )
    {
        return BulkQuery.QueryAsync(
            ids,
            GetChunk,
            degreeOfParalllelism: 100,
            progress: progress,
            cancellationToken: cancellationToken
        );

        async Task<IReadOnlyCollection<string>> GetChunk(IReadOnlyCollection<int> chunk, CancellationToken cancellationToken)
        {
            var request = new BulkRequest("/v2/commerce/prices") { Ids = chunk };
            var json = await request.SendAsync(http, cancellationToken);
            return json.Indent(false)
                .RootElement.EnumerateArray()
                .Select(
                    item => item.ToString()
                        ?? throw new InvalidOperationException("Unexpected null in JSON array.")
                )
                .ToList();
        }
    }
}
