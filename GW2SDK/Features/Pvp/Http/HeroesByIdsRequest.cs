﻿using GuildWars2.Http;
using GuildWars2.Json;
using GuildWars2.Pvp.Heroes;

namespace GuildWars2.Pvp.Http;

internal sealed class HeroesByIdsRequest : IHttpRequest<HashSet<Hero>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/pvp/heroes") { AcceptEncoding = "gzip" };

    public HeroesByIdsRequest(IReadOnlyCollection<string> heroIds)
    {
        Check.Collection(heroIds);
        HeroIds = heroIds;
    }

    public IReadOnlyCollection<string> HeroIds { get; }

    public Language? Language { get; init; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<(HashSet<Hero> Value, MessageContext Context)> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "ids", HeroIds },
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
        var value = json.RootElement.GetSet(entry => entry.GetHero(MissingMemberBehavior));
        return (value, new MessageContext(response));
    }
}
