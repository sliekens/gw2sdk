using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.Colors.Http;

internal sealed class ColorsByIdsRequest : IHttpRequest<Replica<HashSet<Dye>>>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "v2/colors")
    {
        AcceptEncoding = "gzip"
    };

    public ColorsByIdsRequest(IReadOnlyCollection<int> colorIds)
    {
        Check.Collection(colorIds);
        ColorIds = colorIds;
    }

    public IReadOnlyCollection<int> ColorIds { get; }

    public Language? Language { get; init; }

    public MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<Replica<HashSet<Dye>>> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "ids", ColorIds },
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
        return new Replica<HashSet<Dye>>
        {
            Value = json.RootElement.GetSet(entry => entry.GetDye(MissingMemberBehavior)),
            ResultContext = response.Headers.GetResultContext(),
            PageContext = response.Headers.GetPageContext(),
            Date = response.Headers.Date.GetValueOrDefault(),
            Expires = response.Content.Headers.Expires,
            LastModified = response.Content.Headers.LastModified
        };
    }
}
