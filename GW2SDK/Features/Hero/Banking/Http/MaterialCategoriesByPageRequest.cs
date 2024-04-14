using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.Hero.Banking.Http;

internal sealed class MaterialCategoriesByPageRequest(int pageIndex)
    : IHttpRequest<HashSet<MaterialCategory>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/materials") { AcceptEncoding = "gzip" };

    public int PageIndex { get; } = pageIndex;

    public int? PageSize { get; init; }

    public Language? Language { get; init; }

    
    public async Task<(HashSet<MaterialCategory> Value, MessageContext Context)> SendAsync(
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
        var value =
            json.RootElement.GetSet(static entry => entry.GetMaterialCategory());
        return (value, new MessageContext(response));
    }
}
