using System.Net.Http;
using GW2SDK.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Crafting.Http;

[PublicAPI]
public sealed class RecipesByIngredientItemIdRequest
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "/v2/recipes/search")
    {
        AcceptEncoding = "gzip"
    };

    public RecipesByIngredientItemIdRequest(int ingredientItemId)
    {
        IngredientItemId = ingredientItemId;
    }

    public int IngredientItemId { get; }

    public static implicit operator HttpRequestMessage(RecipesByIngredientItemIdRequest r)
    {
        QueryBuilder search = new();
        search.Add("input", r.IngredientItemId);
        search.Add("ids", "all");
        var request = Template with
        {
            Arguments = search
        };
        return request.Compile();
    }
}
