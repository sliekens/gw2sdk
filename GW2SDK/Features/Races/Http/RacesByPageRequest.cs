using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.Races.Http;

internal sealed class RacesByPageRequest : IHttpRequest<Replica<HashSet<Race>>>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "v2/races")
    {
        AcceptEncoding = "gzip"
    };

    public RacesByPageRequest(int pageIndex)
    {
        PageIndex = pageIndex;
    }

    public int PageIndex { get; }

    public int? PageSize { get; init; }

    public Language? Language { get; init; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<Replica<HashSet<Race>>> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        QueryBuilder search = new()
        {
            { "page", PageIndex },
            { "v", SchemaVersion.Recommended }
        };
        if (PageSize.HasValue)
        {
            search.Add("page_size", PageSize.Value);
        }

        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = search,
                    AcceptLanguage = Language?.Alpha2Code
                },
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);
        using var json = await response.Content.ReadAsJsonAsync(cancellationToken).ConfigureAwait(false);
        var value = json.RootElement.GetSet(entry => entry.GetRace(MissingMemberBehavior));
        return new Replica<HashSet<Race>>
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
