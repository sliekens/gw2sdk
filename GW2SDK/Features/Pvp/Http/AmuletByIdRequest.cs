using GuildWars2.Http;
using GuildWars2.Pvp.Amulets;

namespace GuildWars2.Pvp.Http;

internal sealed class AmuletByIdRequest : IHttpRequest<Replica<Amulet>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/pvp/amulets") { AcceptEncoding = "gzip" };

    public AmuletByIdRequest(int amuletId)
    {
        AmuletId = amuletId;
    }

    public int AmuletId { get; }

    public Language? Language { get; init; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<Replica<Amulet>> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "id", AmuletId },
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
        return new Replica<Amulet>
        {
            Value = json.RootElement.GetAmulet(MissingMemberBehavior),
            ResultContext = response.Headers.GetResultContext(),
            PageContext = response.Headers.GetPageContext(),
            Date = response.Headers.Date.GetValueOrDefault(),
            Expires = response.Content.Headers.Expires,
            LastModified = response.Content.Headers.LastModified
        };
    }
}
