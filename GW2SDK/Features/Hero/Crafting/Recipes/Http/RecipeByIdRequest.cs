using GuildWars2.Http;

namespace GuildWars2.Hero.Crafting.Recipes.Http;

internal sealed class RecipeByIdRequest(int recipeId) : IHttpRequest<Recipe>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "v2/recipes")
    {
        AcceptEncoding = "gzip"
    };

    public int RecipeId { get; } = recipeId;

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<(Recipe Value, MessageContext Context)> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "id", RecipeId },
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
        var value = json.RootElement.GetRecipe(MissingMemberBehavior);
        return (value, new MessageContext(response));
    }
}
