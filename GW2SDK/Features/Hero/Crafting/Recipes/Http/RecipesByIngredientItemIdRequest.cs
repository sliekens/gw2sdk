using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.Hero.Crafting.Recipes.Http;

internal sealed class RecipesByIngredientItemIdRequest
    (int ingredientItemId) : IHttpRequest<HashSet<Recipe>>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "v2/recipes/search")
    {
        AcceptEncoding = "gzip"
    };

    public int IngredientItemId { get; } = ingredientItemId;

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
                        { "input", IngredientItemId },
                        { "ids", "all" },
                        { "v", SchemaVersion.Recommended }
                    }
                },
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
