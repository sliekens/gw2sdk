using System.Net.Http;
using GW2SDK.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Crafting.Http;

[PublicAPI]
public sealed class RecipesByIngredientItemIdByPageRequest
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "/v2/recipes/search")
    {
        AcceptEncoding = "gzip"
    };

    public RecipesByIngredientItemIdByPageRequest(
        int ingredientItemId,
        int pageIndex,
        int? pageSize
    )
    {
        IngredientItemId = ingredientItemId;
        PageIndex = pageIndex;
        PageSize = pageSize;
    }

    public int IngredientItemId { get; }

    public int PageIndex { get; }

    public int? PageSize { get; }

    public static implicit operator HttpRequestMessage(RecipesByIngredientItemIdByPageRequest r)
    {
        QueryBuilder search = new();
        search.Add("input", r.IngredientItemId);
        search.Add("page", r.PageIndex);
        if (r.PageSize.HasValue)
        {
            search.Add("page_size", r.PageSize.Value);
        }

        var request = Template with
        {
            Arguments = search
        };
        return request.Compile();
    }
}
