using GuildWars2.Http;
using GuildWars2.Json;
using GuildWars2.Wvw.Abilities;

namespace GuildWars2.Wvw.Http;

internal sealed class AbilitiesByPageRequest : IHttpRequest2<HashSet<Ability>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/wvw/abilities") { AcceptEncoding = "gzip" };

    public AbilitiesByPageRequest(int pageIndex)
    {
        PageIndex = pageIndex;
    }

    public int PageIndex { get; }

    public int? PageSize { get; init; }

    public Language? Language { get; init; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<(HashSet<Ability> Value, MessageContext Context)> SendAsync(
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
        using var json = await response.Content.ReadAsJsonAsync(cancellationToken).ConfigureAwait(false);
        var value = json.RootElement.GetSet(entry => entry.GetAbility(MissingMemberBehavior));
        return (value, new MessageContext(response));
    }
}
