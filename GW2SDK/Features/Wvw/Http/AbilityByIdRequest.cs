using GuildWars2.Http;
using GuildWars2.Wvw.Abilities;

namespace GuildWars2.Wvw.Http;

internal sealed class AbilityByIdRequest : IHttpRequest<Replica<Ability>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/wvw/abilities") { AcceptEncoding = "gzip" };

    public AbilityByIdRequest(int abilityId)
    {
        AbilityId = abilityId;
    }

    public int AbilityId { get; }

    public Language? Language { get; init; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<Replica<Ability>> SendAsync(
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
        return new Replica<Ability>
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
