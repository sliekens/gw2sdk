using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Crafting.Json;
using GW2SDK.Crafting.Models;
using GW2SDK.Http;
using GW2SDK.Json;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Crafting.Http;

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

    public async Task<IReplicaSet<Recipe>> SendAsync(HttpClient httpClient, CancellationToken cancellationToken)
    {
        QueryBuilder search = new();
        search.Add("ids", RecipeIds);
        var request = Template with
        {
            Arguments = search
        };

        using var response = await httpClient.SendAsync(request.Compile(),
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken)
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken)
            .ConfigureAwait(false);

        using var json = await response.Content.ReadAsJsonAsync(cancellationToken)
            .ConfigureAwait(false);

        var value = json.RootElement.GetSet(entry => RecipeReader.Read(entry, MissingMemberBehavior));
        return new ReplicaSet<Recipe>(response.Headers.Date.GetValueOrDefault(),
            value,
            response.Headers.GetCollectionContext(),
            response.Content.Headers.Expires,
            response.Content.Headers.LastModified);
    }
}
