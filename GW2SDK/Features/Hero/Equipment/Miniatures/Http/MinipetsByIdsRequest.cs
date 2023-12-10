﻿using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.Hero.Equipment.Miniatures.Http;

internal sealed class MinipetsByIdsRequest : IHttpRequest<HashSet<Minipet>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/minis") { AcceptEncoding = "gzip" };

    public MinipetsByIdsRequest(IReadOnlyCollection<int> minipetIds)
    {
        Check.Collection(minipetIds);
        MinipetIds = minipetIds;
    }

    public IReadOnlyCollection<int> MinipetIds { get; }

    public Language? Language { get; init; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<(HashSet<Minipet> Value, MessageContext Context)> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "ids", MinipetIds },
                        { "v", SchemaVersion.Recommended }
                    },
                    AcceptLanguage = Language?.Alpha2Code
                },
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);
        using var json = await response.Content.ReadAsJsonAsync(cancellationToken).ConfigureAwait(false);
        var value = json.RootElement.GetSet(entry => entry.GetMinipet(MissingMemberBehavior));
        return (value, new MessageContext(response));
    }
}