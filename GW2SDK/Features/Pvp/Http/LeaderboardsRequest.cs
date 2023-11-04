﻿using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.Pvp.Http;

internal sealed class LeaderboardsRequest : IHttpRequest2<HashSet<string>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/pvp/seasons/:id/leaderboards") { AcceptEncoding = "gzip" };

    public LeaderboardsRequest(string seasonId)
    {
        SeasonId = seasonId;
    }

    public string SeasonId { get; }

    public async Task<(HashSet<string> Value, MessageContext Context)> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Path = Template.Path.Replace(":id", SeasonId),
                    Arguments = new QueryBuilder { { "v", SchemaVersion.Recommended } }
                },
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);
        using var json = await response.Content.ReadAsJsonAsync(cancellationToken).ConfigureAwait(false);
        var value = json.RootElement.GetSet(entry => entry.GetStringRequired());
        return (value, new MessageContext(response));
    }
}
