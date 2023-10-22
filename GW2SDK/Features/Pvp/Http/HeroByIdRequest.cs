using GuildWars2.Http;
using GuildWars2.Pvp.Heroes;

namespace GuildWars2.Pvp.Http;

internal sealed class HeroByIdRequest : IHttpRequest<Replica<Hero>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/pvp/heroes") { AcceptEncoding = "gzip" };

    public HeroByIdRequest(string heroId)
    {
        HeroId = heroId;
    }

    public string HeroId { get; }

    public Language? Language { get; init; }

    public MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<Replica<Hero>> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "id", HeroId },
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
        return new Replica<Hero>
        {
            Value = json.RootElement.GetHero(MissingMemberBehavior),
            ResultContext = response.Headers.GetResultContext(),
            PageContext = response.Headers.GetPageContext(),
            Date = response.Headers.Date.GetValueOrDefault(),
            Expires = response.Content.Headers.Expires,
            LastModified = response.Content.Headers.LastModified
        };
    }
}
