﻿using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.Pvp.Standings.Http;

internal sealed class StandingsRequest : IHttpRequest<HashSet<Standing>>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "v2/pvp/standings")
    {
        AcceptEncoding = "gzip",
        Arguments = new QueryBuilder { { "v", SchemaVersion.Recommended } }
    };

    public required string? AccessToken { get; init; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<(HashSet<Standing> Value, MessageContext Context)> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with { BearerToken = AccessToken },
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);
        using var json = await response.Content.ReadAsJsonAsync(cancellationToken)
            .ConfigureAwait(false);

        var value = json.RootElement.GetSet(entry => entry.GetStanding(MissingMemberBehavior));
        return (value, new MessageContext(response));
    }
}
