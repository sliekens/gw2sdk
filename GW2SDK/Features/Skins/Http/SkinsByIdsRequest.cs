﻿using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Http;
using GW2SDK.Json;
using GW2SDK.Skins.Json;
using GW2SDK.Skins.Models;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Skins.Http;

[PublicAPI]
public sealed class SkinsByIdsRequest : IHttpRequest<IReplicaSet<Skin>>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "/v2/skins")
    {
        AcceptEncoding = "gzip"
    };

    public SkinsByIdsRequest(IReadOnlyCollection<int> skinIds)
    {
        Check.Collection(skinIds, nameof(skinIds));
        SkinIds = skinIds;
    }

    public IReadOnlyCollection<int> SkinIds { get; }

    public Language? Language { get; init; }

    public MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<IReplicaSet<Skin>> SendAsync(HttpClient httpClient, CancellationToken cancellationToken)
    {
        QueryBuilder search = new();
        search.Add("ids", SkinIds);
        var request = Template with
        {
            Arguments = search,
            AcceptLanguage = Language?.Alpha2Code
        };

        using var response = await httpClient.SendAsync(request.Compile(),
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken)
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken)
            .ConfigureAwait(false);

        using var json = await response.Content.ReadAsJsonAsync(cancellationToken)
            .ConfigureAwait(false);

        var value = json.RootElement.GetSet(entry => SkinReader.Read(entry, MissingMemberBehavior));
        return new ReplicaSet<Skin>(response.Headers.Date.GetValueOrDefault(),
            value,
            response.Headers.GetCollectionContext(),
            response.Content.Headers.Expires,
            response.Content.Headers.LastModified);
    }
}
