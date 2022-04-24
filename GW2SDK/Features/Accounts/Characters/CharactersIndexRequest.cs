﻿using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Http;
using GW2SDK.Json;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Accounts.Characters;

[PublicAPI]
public sealed class CharactersIndexRequest : IHttpRequest<IReplicaSet<string>>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "/v2/characters")
    {
        AcceptEncoding = "gzip"
    };

    public string? AccessToken { get; init; }

    public async Task<IReplicaSet<string>> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        var request = Template with { BearerToken = AccessToken };
        using var response = await httpClient.SendAsync(
                request.Compile(),
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken
                )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);

        using var json = await response.Content.ReadAsJsonAsync(cancellationToken)
            .ConfigureAwait(false);

        var value = json.RootElement.GetSet(entry => entry.GetStringRequired());
        return new ReplicaSet<string>(
            response.Headers.Date.GetValueOrDefault(),
            value,
            response.Headers.GetCollectionContext(),
            response.Content.Headers.Expires,
            response.Content.Headers.LastModified
            );
    }
}