﻿using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Http;
using GW2SDK.Json;
using GW2SDK.Maps.Json;
using GW2SDK.Maps.Models;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Maps.Http;

[PublicAPI]
public sealed class ContinentsByIdsRequest : IHttpRequest<IReplicaSet<Continent>>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "/v2/continents")
    {
        AcceptEncoding = "gzip"
    };

    public ContinentsByIdsRequest(IReadOnlyCollection<int> continentIds)
    {
        Check.Collection(continentIds, nameof(continentIds));
        ContinentIds = continentIds;
    }

    public IReadOnlyCollection<int> ContinentIds { get; }

    public Language? Language { get; init; }

    public MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<IReplicaSet<Continent>> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        QueryBuilder search = new();
        search.Add("ids", ContinentIds);
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

        var value =
            json.RootElement.GetSet(entry => ContinentReader.Read(entry, MissingMemberBehavior));
        return new ReplicaSet<Continent>(
            response.Headers.Date.GetValueOrDefault(),
            value,
            response.Headers.GetCollectionContext(),
            response.Content.Headers.Expires,
            response.Content.Headers.LastModified
            );
    }
}