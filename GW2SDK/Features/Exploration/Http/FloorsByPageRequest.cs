using System.Globalization;
using GuildWars2.Exploration.Floors;
using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.Exploration.Http;

internal sealed class FloorsByPageRequest : IHttpRequest<HashSet<Floor>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/continents/:id/floors") { AcceptEncoding = "gzip" };

    public FloorsByPageRequest(int continentId, int pageIndex)
    {
        ContinentId = continentId;
        PageIndex = pageIndex;
    }

    public int ContinentId { get; }

    public int PageIndex { get; }

    public int? PageSize { get; init; }

    public Language? Language { get; init; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<(HashSet<Floor> Value, MessageContext Context)> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        QueryBuilder search = new() { { "page", PageIndex } };
        if (PageSize.HasValue)
        {
            search.Add("page_size", PageSize.Value);
        }

        search.Add("v", SchemaVersion.Recommended);
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Path = Template.Path.Replace(
                        ":id",
                        ContinentId.ToString(CultureInfo.InvariantCulture)
                    ),
                    Arguments = search,
                    AcceptLanguage = Language?.Alpha2Code
                },
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);
        using var json = await response.Content.ReadAsJsonAsync(cancellationToken)
            .ConfigureAwait(false);
        var value = json.RootElement.GetSet(entry => entry.GetFloor(MissingMemberBehavior));
        return (value, new MessageContext(response));
    }
}
