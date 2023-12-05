﻿using GuildWars2.Http;
using GuildWars2.Wvw.Abilities;

namespace GuildWars2.Wvw.Http;

internal sealed class AbilityByIdRequest(int abilityId) : IHttpRequest<Ability>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/wvw/abilities") { AcceptEncoding = "gzip" };

    public int AbilityId { get; } = abilityId;

    public Language? Language { get; init; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<(Ability Value, MessageContext Context)> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "id", AbilityId },
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
        var value = json.RootElement.GetAbility(MissingMemberBehavior);
        return (value, new MessageContext(response));
    }
}
