using System.Collections.Generic;
using System.Net.Http;
using GW2SDK.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Crafting.Http;

[PublicAPI]
public sealed class RecipesByIdsRequest
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "/v2/recipes")
    {
        AcceptEncoding = "gzip"
    };

    public RecipesByIdsRequest(IReadOnlyCollection<int> recipeIds)
    {
        Check.Collection(recipeIds, nameof(recipeIds));
        RecipeIds = recipeIds;
    }

    public IReadOnlyCollection<int> RecipeIds { get; }

    public static implicit operator HttpRequestMessage(RecipesByIdsRequest r)
    {
        QueryBuilder search = new();
        search.Add("ids", r.RecipeIds);
        var request = Template with
        {
            Arguments = search
        };
        return request.Compile();
    }
}
