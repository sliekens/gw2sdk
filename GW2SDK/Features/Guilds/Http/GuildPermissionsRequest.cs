﻿using GuildWars2.Guilds.Permissions;
using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.Guilds.Http;

internal sealed class GuildPermissionsRequest : IHttpRequest<HashSet<GuildPermissionSummary>>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "v2/guild/permissions")
    {
        AcceptEncoding = "gzip",
        Arguments = new QueryBuilder
        {
            { "ids", "all" },
            { "v", SchemaVersion.Recommended }
        }
    };

    public Language? Language { get; init; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<(HashSet<GuildPermissionSummary> Value, MessageContext Context)> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with { AcceptLanguage = Language?.Alpha2Code },
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);
        using var json = await response.Content.ReadAsJsonAsync(cancellationToken)
            .ConfigureAwait(false);
        var value = json.RootElement.GetSet(
            entry => entry.GetGuildPermissionSummary(MissingMemberBehavior)
        );
        return (value, new MessageContext(response));
    }
}
