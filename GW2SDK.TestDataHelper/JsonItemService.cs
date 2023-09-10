﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GuildWars2.Items;

namespace GuildWars2.TestDataHelper;

public class JsonItemService
{
    private readonly HttpClient http;

    public JsonItemService(HttpClient http)
    {
        this.http = http;
    }

    public async Task<ISet<string>> GetAllJsonItems(IProgress<ResultContext> progress)
    {
        var ids = await GetItemsIndex().ConfigureAwait(false);
        var items = new SortedSet<string>();
        await foreach (var item in GetJsonItemsByIds(ids, progress))
        {
            items.Add(item);
        }

        return items;
    }

    private async Task<HashSet<int>> GetItemsIndex() =>
        await new ItemsIndexRequest().SendAsync(http, CancellationToken.None);

    public IAsyncEnumerable<string> GetJsonItemsByIds(
        IReadOnlyCollection<int> itemIds,
        IProgress<ResultContext>? progress = default,
        CancellationToken cancellationToken = default
    )
    {
        var producer = BulkQuery.Create<int, string>(
            async (chunk, ct) =>
            {
                var request = new BulkRequest("/v2/items") { Ids = chunk };
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
