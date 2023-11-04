﻿using GuildWars2.Http;

namespace GuildWars2.Raids.Http;

internal sealed class RaidByIdRequest : IHttpRequest<Replica<Raid>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/raids") { AcceptEncoding = "gzip" };

    public RaidByIdRequest(string raidId)
    {
        RaidId = raidId;
    }

    public string RaidId { get; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<Replica<Raid>> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "id", RaidId },
                        { "v", SchemaVersion.Recommended }
                    }
                },
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);
        using var json = await response.Content.ReadAsJsonAsync(cancellationToken).ConfigureAwait(false);
        var value = json.RootElement.GetRaid(MissingMemberBehavior);
        return new Replica<Raid>
        {
            Value = value,
            ResultContext = response.Headers.GetResultContext(),
            PageContext = response.Headers.GetPageContext(),
            Date = response.Headers.Date.GetValueOrDefault(),
            Expires = response.Content.Headers.Expires,
            LastModified = response.Content.Headers.LastModified
        };
    }
}
