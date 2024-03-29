﻿using GuildWars2.Http;
using GuildWars2.Json;
using GuildWars2.Wvw.Abilities;

namespace GuildWars2.Wvw.Http;

internal sealed class AbilitiesByIdsRequest : IHttpRequest<HashSet<Ability>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/wvw/abilities") { AcceptEncoding = "gzip" };

    public AbilitiesByIdsRequest(IReadOnlyCollection<int> abilityIds)
    {
        Check.Collection(abilityIds);
        AbilityIds = abilityIds;
    }

    public IReadOnlyCollection<int> AbilityIds { get; }

    public Language? Language { get; init; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<(HashSet<Ability> Value, MessageContext Context)> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "ids", AbilityIds },
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
        var value = json.RootElement.GetSet(entry => entry.GetAbility(MissingMemberBehavior));
        return (value, new MessageContext(response));
    }
}
