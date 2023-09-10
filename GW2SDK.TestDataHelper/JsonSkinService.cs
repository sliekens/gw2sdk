using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GuildWars2.Skins;

namespace GuildWars2.TestDataHelper;

public class JsonSkinService
{
    private readonly HttpClient http;

    public JsonSkinService(HttpClient http)
    {
        this.http = http;
    }

    public async Task<ISet<string>> GetAllJsonSkins(IProgress<ResultContext> progress)
    {
        var ids = await GetSkinIds().ConfigureAwait(false);
        var items = new SortedSet<string>();
        await foreach (var item in GetJsonSkinsByIds(ids, progress))
        {
            items.Add(item);
        }

        return items;
    }

    private async Task<HashSet<int>> GetSkinIds() =>
        await new SkinsIndexRequest().SendAsync(http, CancellationToken.None);

    public IAsyncEnumerable<string> GetJsonSkinsByIds(
        IReadOnlyCollection<int> itemIds,
        IProgress<ResultContext>? progress = default,
        CancellationToken cancellationToken = default
    )
    {
        var producer = BulkQuery.Create<int, string>(
            async (chunk, ct) =>
            {
                var request = new BulkRequest("/v2/skins") { Ids = chunk };
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
