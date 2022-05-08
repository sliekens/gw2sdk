using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Http;
using GW2SDK.Json;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Crafting;

[PublicAPI]
public sealed class RecipesByIngredientItemIdByPageRequest : IHttpRequest<IReplicaPage<Recipe>>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "/v2/recipes/search")
    {
        AcceptEncoding = "gzip"
    };

    public RecipesByIngredientItemIdByPageRequest(int ingredientItemId, int pageIndex)
    {
        IngredientItemId = ingredientItemId;
        PageIndex = pageIndex;
    }

    public int IngredientItemId { get; }

    public int PageIndex { get; }

    public int? PageSize { get; init; }

    public MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<IReplicaPage<Recipe>> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        QueryBuilder search = new()
        {
            { "input", IngredientItemId },
            { "page", PageIndex }
        };
        if (PageSize.HasValue)
        {
            search.Add("page_size", PageSize.Value);
        }

        using var response = await httpClient.SendAsync(
                Template with { Arguments = search },
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);

        using var json = await response.Content.ReadAsJsonAsync(cancellationToken)
            .ConfigureAwait(false);

        var value = json.RootElement.GetSet(entry => entry.GetRecipe(MissingMemberBehavior));
        return new ReplicaPage<Recipe>(
            response.Headers.Date.GetValueOrDefault(),
            value,
            response.Headers.GetPageContext(),
            response.Content.Headers.Expires,
            response.Content.Headers.LastModified
        );
    }
}
