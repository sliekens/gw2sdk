﻿using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Achievements.Json;
using GW2SDK.Achievements.Models;
using GW2SDK.Http;
using GW2SDK.Json;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Achievements.Http;

[PublicAPI]
public sealed class TitlesByIdsRequest : IHttpRequest<IReplicaSet<Title>>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "/v2/titles")
    {
        AcceptEncoding = "gzip"
    };

    public TitlesByIdsRequest(IReadOnlyCollection<int> titleIds)
    {
        Check.Collection(titleIds, nameof(titleIds));
        TitleIds = titleIds;
    }

    public IReadOnlyCollection<int> TitleIds { get; }

    public Language? Language { get; init; }

    public MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<IReplicaSet<Title>> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        QueryBuilder search = new();
        search.Add("ids", TitleIds);
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
            json.RootElement.GetSet(entry => TitleReader.Read(entry, MissingMemberBehavior));
        return new ReplicaSet<Title>(
            response.Headers.Date.GetValueOrDefault(),
            value,
            response.Headers.GetCollectionContext(),
            response.Content.Headers.Expires,
            response.Content.Headers.LastModified
            );
    }
}
