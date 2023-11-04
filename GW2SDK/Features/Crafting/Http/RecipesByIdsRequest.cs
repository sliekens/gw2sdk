using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.Crafting.Http;

internal sealed class RecipesByIdsRequest : IHttpRequest<HashSet<Recipe>>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "v2/recipes")
    {
        AcceptEncoding = "gzip"
    };

    public RecipesByIdsRequest(IReadOnlyCollection<int> recipeIds)
    {
        Check.Collection(recipeIds);
        RecipeIds = recipeIds;
    }

    public IReadOnlyCollection<int> RecipeIds { get; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<(HashSet<Recipe> Value, MessageContext Context)> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "ids", RecipeIds },
                        { "v", SchemaVersion.Recommended }
                    }
                },
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);
        using var json = await response.Content.ReadAsJsonAsync(cancellationToken).ConfigureAwait(false);
        var value = json.RootElement.GetSet(entry => entry.GetRecipe(MissingMemberBehavior));
        return (value, new MessageContext(response));
    }
}
