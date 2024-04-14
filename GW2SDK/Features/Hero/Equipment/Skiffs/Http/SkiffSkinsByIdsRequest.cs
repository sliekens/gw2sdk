﻿using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.Hero.Equipment.Skiffs.Http;

internal sealed class SkiffSkinsByIdsRequest : IHttpRequest<HashSet<SkiffSkin>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/skiffs") { AcceptEncoding = "gzip" };

    public SkiffSkinsByIdsRequest(IReadOnlyCollection<int> skiffSkinIds)
    {
        Check.Collection(skiffSkinIds);
        SkiffSkinIds = skiffSkinIds;
    }

    public IReadOnlyCollection<int> SkiffSkinIds { get; }

    public Language? Language { get; init; }

    
    public async Task<(HashSet<SkiffSkin> Value, MessageContext Context)> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "ids", SkiffSkinIds },
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
        var value = json.RootElement.GetSet(static entry => entry.GetSkiffSkin());
        return (value, new MessageContext(response));
    }
}
