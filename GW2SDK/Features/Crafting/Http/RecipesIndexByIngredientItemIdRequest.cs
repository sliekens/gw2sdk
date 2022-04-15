using System.Net.Http;
using GW2SDK.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Crafting.Http;

[PublicAPI]
public sealed class RecipesIndexByIngredientItemIdRequest
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "/v2/recipes/search")
    {
        AcceptEncoding = "gzip"
    };

    public RecipesIndexByIngredientItemIdRequest(int ingredientItemId)
    {
        IngredientItemId = ingredientItemId;
    }

    public int IngredientItemId { get; }

    public static implicit operator HttpRequestMessage(RecipesIndexByIngredientItemIdRequest r)
    {
        QueryBuilder search = new();
        search.Add("input", r.IngredientItemId);
        var request = Template with
        {
            Arguments = search
        };
        return request.Compile();
    }
}
