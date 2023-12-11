using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.Hero.Crafting.Recipes.Http;

internal sealed class RecipesByOutputItemIdByPageRequest(int outputItemId, int pageIndex)
    : IHttpRequest<HashSet<Recipe>>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "v2/recipes/search")
    {
        AcceptEncoding = "gzip"
    };

    public int OutputItemId { get; } = outputItemId;

    public int PageIndex { get; } = pageIndex;

    public int? PageSize { get; init; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<(HashSet<Recipe> Value, MessageContext Context)> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        QueryBuilder search = new()
        {
            { "output", OutputItemId },
            { "page", PageIndex }
        };
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
        var value = json.RootElement.GetSet(entry => entry.GetRecipe(MissingMemberBehavior));
        return (value, new MessageContext(response));
    }
}
