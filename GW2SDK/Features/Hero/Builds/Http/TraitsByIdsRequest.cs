﻿using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.Hero.Builds.Http;

internal sealed class TraitsByIdsRequest : IHttpRequest<HashSet<Trait>>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "v2/traits")
    {
        AcceptEncoding = "gzip"
    };

    public TraitsByIdsRequest(IReadOnlyCollection<int> traitIds)
    {
        Check.Collection(traitIds);
        TraitIds = traitIds;
    }

    public IReadOnlyCollection<int> TraitIds { get; }

    public Language? Language { get; init; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<(HashSet<Trait> Value, MessageContext Context)> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "ids", TraitIds },
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
        var value = json.RootElement.GetSet(entry => entry.GetTrait(MissingMemberBehavior));
        return (value, new MessageContext(response));
    }
}
