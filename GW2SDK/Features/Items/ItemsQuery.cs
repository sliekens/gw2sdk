﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace GuildWars2.Items;

[PublicAPI]
public sealed class ItemsQuery
{
    private readonly HttpClient http;

    public ItemsQuery(HttpClient http)
    {
        this.http = http ?? throw new ArgumentNullException(nameof(http));
        http.BaseAddress ??= BaseAddress.DefaultUri;
    }

    public Task<Replica<HashSet<int>>> GetItemsIndex(CancellationToken cancellationToken = default)
    {
        var request = new ItemsIndexRequest();
        return request.SendAsync(http, cancellationToken);
    }

    public Task<Replica<Item>> GetItemById(
        int itemId,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        ItemByIdRequest request = new(itemId)
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<Replica<HashSet<Item>>> GetItemsByIds(
        IReadOnlyCollection<int> itemIds,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        ItemsByIdsRequest request = new(itemIds)
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<Replica<HashSet<Item>>> GetItemsByPage(
        int pageIndex,
        int? pageSize = default,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        ItemsByPageRequest request = new(pageIndex)
        {
            PageSize = pageSize,
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };

        return request.SendAsync(http, cancellationToken);
    }

    public IAsyncEnumerable<Item> GetItemsBulk(
        IReadOnlyCollection<int> itemIds,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        int degreeOfParalllelism = BulkQuery.DefaultDegreeOfParalllelism,
        int chunkSize = BulkQuery.DefaultChunkSize,
        IProgress<ResultContext>? progress = default,
        CancellationToken cancellationToken = default
    )
    {
        var producer = BulkQuery.Create<int, Item>(
            async (chunk, ct) =>
            {
                var response = await GetItemsByIds(chunk, language, missingMemberBehavior, ct)
                    .ConfigureAwait(false);
                return response.Value;
            }
        );
        return producer.QueryAsync(
            itemIds,
            degreeOfParalllelism,
            chunkSize,
            progress,
            cancellationToken
        );
    }

    public async IAsyncEnumerable<Item> GetItemsBulk(
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        int degreeOfParalllelism = BulkQuery.DefaultDegreeOfParalllelism,
        int chunkSize = BulkQuery.DefaultChunkSize,
        IProgress<ResultContext>? progress = default,
        [EnumeratorCancellation] CancellationToken cancellationToken = default
    )
    {
        var index = await GetItemsIndex(cancellationToken).ConfigureAwait(false);
        var producer = GetItemsBulk(
            index.Value,
            language,
            missingMemberBehavior,
            degreeOfParalllelism,
            chunkSize,
            progress,
            cancellationToken
        );
        await foreach (var item in producer.WithCancellation(cancellationToken)
            .ConfigureAwait(false))
        {
            yield return item;
        }
    }
}
