﻿using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Http;
using GW2SDK.Json;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Maps;

[PublicAPI]
public sealed class WorldByIdRequest : IHttpRequest<IReplica<World>>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "/v2/worlds")
    {
        AcceptEncoding = "gzip"
    };

    public WorldByIdRequest(int worldId)
    {
        WorldId = worldId;
    }

    public int WorldId { get; }

    public Language? Language { get; init; }

    public MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<IReplica<World>> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        QueryBuilder search = new();
        search.Add("id", WorldId);
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

        var value = json.RootElement.GetWorld(MissingMemberBehavior);
        return new Replica<World>(
            response.Headers.Date.GetValueOrDefault(),
            value,
            response.Content.Headers.Expires,
            response.Content.Headers.LastModified
            );
    }
}