using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.Quaggans.Http;

internal sealed class QuaggansByPageRequest(int pageIndex) : IHttpRequest<HashSet<Quaggan>>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "v2/quaggans")
    {
        AcceptEncoding = "gzip"
    };

    public int PageIndex { get; } = pageIndex;

    public int? PageSize { get; init; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<(HashSet<Quaggan> Value, MessageContext Context)> SendAsync(
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
                Template with { Arguments = search },
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);
        using var json = await response.Content.ReadAsJsonAsync(cancellationToken)
            .ConfigureAwait(false);
        var value = json.RootElement.GetSet(entry => entry.GetQuaggan(MissingMemberBehavior));
        return (value, new MessageContext(response));
    }
}
