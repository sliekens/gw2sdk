using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GuildWars2.Http;
using GuildWars2.Json;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GuildWars2.Crafting;

[PublicAPI]
public sealed class RecipesByOutputItemIdRequest : IHttpRequest<IReplicaSet<Recipe>>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "v2/recipes/search")
    {
        AcceptEncoding = "gzip"
    };

    public RecipesByOutputItemIdRequest(int outputItemId)
    {
        OutputItemId = outputItemId;
    }

    public int OutputItemId { get; }

    public MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<IReplicaSet<Recipe>> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        QueryBuilder search = new()
        {
            { "ids", "all" },
            { "output", OutputItemId },
            { "v", SchemaVersion.Recommended }
        };
        using var response = await httpClient.SendAsync(
                Template with { Arguments = search },
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);

        using var json = await response.Content.ReadAsJsonAsync(cancellationToken)
            .ConfigureAwait(false);

        var value = json.RootElement.GetSet(entry => RecipeJson.GetRecipe(entry, MissingMemberBehavior));
        return new ReplicaSet<Recipe>(
            response.Headers.Date.GetValueOrDefault(),
            value,
            response.Headers.GetCollectionContext(),
            response.Content.Headers.Expires,
            response.Content.Headers.LastModified
        );
    }
}
