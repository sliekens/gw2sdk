using GuildWars2.Http;

namespace GuildWars2.Crafting.Http;

internal sealed class RecipeByIdRequest : IHttpRequest<Recipe>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "v2/recipes")
    {
        AcceptEncoding = "gzip"
    };

    public RecipeByIdRequest(int recipeId)
    {
        RecipeId = recipeId;
    }

    public int RecipeId { get; }

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
        using var json = await response.Content.ReadAsJsonAsync(cancellationToken).ConfigureAwait(false);
        var value = json.RootElement.GetRecipe(MissingMemberBehavior);
        return (value, new MessageContext(response));
    }
}
