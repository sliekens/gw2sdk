using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.Wvw.Abilities;

[PublicAPI]
public sealed class AbilitiesByIdsRequest : IHttpRequest<Replica<HashSet<Ability>>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(HttpMethod.Get, "v2/wvw/abilities") { AcceptEncoding = "gzip" };

    public AbilitiesByIdsRequest(IReadOnlyCollection<int> abilityIds)
    {
        Check.Collection(abilityIds);
        AbilityIds = abilityIds;
    }

    public IReadOnlyCollection<int> AbilityIds { get; }

    public Language? Language { get; init; }

    public MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<Replica<HashSet<Ability>>> SendAsync(
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
        return new Replica<HashSet<Ability>>
        {
            Value = json.RootElement.GetSet(entry => entry.GetAbility(MissingMemberBehavior)),
            ResultContext = response.Headers.GetResultContext(),
            PageContext = response.Headers.GetPageContext(),
            Date = response.Headers.Date.GetValueOrDefault(),
            Expires = response.Content.Headers.Expires,
            LastModified = response.Content.Headers.LastModified
        };
    }
}
