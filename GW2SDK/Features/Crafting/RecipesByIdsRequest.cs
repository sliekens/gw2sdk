using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Http;
using GW2SDK.Json;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Crafting;

[PublicAPI]
public sealed class RecipesByIdsRequest : IHttpRequest<IReplicaSet<Recipe>>
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

    public MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<IReplicaSet<Recipe>> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with { Arguments = new QueryBuilder { { "ids", RecipeIds }, { "v", SchemaVersion.Recommended } } },
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);

        using var json = await response.Content.ReadAsJsonAsync(cancellationToken)
            .ConfigureAwait(false);

        var value = json.RootElement.GetSet(entry => entry.GetRecipe(MissingMemberBehavior));
        return new ReplicaSet<Recipe>(
            response.Headers.Date.GetValueOrDefault(),
            value,
            response.Headers.GetCollectionContext(),
            response.Content.Headers.Expires,
            response.Content.Headers.LastModified
        );
    }
}
