﻿using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Banking.Json;
using GW2SDK.Banking.Models;
using GW2SDK.Http;
using GW2SDK.Json;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Banking.Http;

[PublicAPI]
public sealed class MaterialCategoriesByIdsRequest : IHttpRequest<IReplicaSet<MaterialCategory>>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "/v2/materials")
    {
        AcceptEncoding = "gzip"
    };

    public MaterialCategoriesByIdsRequest(IReadOnlyCollection<int> materialCategoriesIds)
    {
        Check.Collection(materialCategoriesIds, nameof(materialCategoriesIds));
        MaterialCategoriesIds = materialCategoriesIds;
    }

    public IReadOnlyCollection<int> MaterialCategoriesIds { get; }

    public Language? Language { get; init; }

    public MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<IReplicaSet<MaterialCategory>> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        QueryBuilder search = new();
        search.Add("ids", MaterialCategoriesIds);
        var request = Template with
        {
            Arguments = search,
            AcceptLanguage = Language?.Alpha2Code
        };

        using var response = await httpClient.SendAsync(
                request.Compile(),
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken
                )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);

        using var json = await response.Content.ReadAsJsonAsync(cancellationToken)
            .ConfigureAwait(false);

        var value = json.RootElement.GetSet(
            entry => MaterialCategoryReader.Read(entry, MissingMemberBehavior)
            );
        return new ReplicaSet<MaterialCategory>(
            response.Headers.Date.GetValueOrDefault(),
            value,
            response.Headers.GetCollectionContext(),
            response.Content.Headers.Expires,
            response.Content.Headers.LastModified
            );
    }
}