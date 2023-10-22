﻿using GuildWars2.Http;
using GuildWars2.Json;
using GuildWars2.Wvw.Upgrades;

namespace GuildWars2.Wvw.Http;

internal sealed class UpgradesByIdsRequest : IHttpRequest<Replica<HashSet<ObjectiveUpgrade>>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/wvw/upgrades") { AcceptEncoding = "gzip" };

    public UpgradesByIdsRequest(IReadOnlyCollection<int> upgradeIds)
    {
        Check.Collection(upgradeIds);
        UpgradeIds = upgradeIds;
    }

    public IReadOnlyCollection<int> UpgradeIds { get; }

    public Language? Language { get; init; }

    public MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<Replica<HashSet<ObjectiveUpgrade>>> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "ids", UpgradeIds },
                        { "v", SchemaVersion.Recommended }
                    },
                    AcceptLanguage = Language?.Alpha2Code
                },
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);
        using var json = await response.Content.ReadAsJsonAsync(cancellationToken)
            .ConfigureAwait(false);
        return new Replica<HashSet<ObjectiveUpgrade>>
        {
            Value =
                json.RootElement.GetSet(
                    entry => entry.GetObjectiveUpgrade(MissingMemberBehavior)
                ),
            ResultContext = response.Headers.GetResultContext(),
            PageContext = response.Headers.GetPageContext(),
            Date = response.Headers.Date.GetValueOrDefault(),
            Expires = response.Content.Headers.Expires,
            LastModified = response.Content.Headers.LastModified
        };
    }
}