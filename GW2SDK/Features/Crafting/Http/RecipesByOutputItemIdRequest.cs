using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.Crafting.Http;

internal sealed class RecipesByOutputItemIdRequest : IHttpRequest<HashSet<Recipe>>
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

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<(HashSet<Recipe> Value, MessageContext Context)> SendAsync(
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
        using var response = await httpClient.SendAsync(Template with { Arguments = search }, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
        await response.EnsureResult(cancellationToken).ConfigureAwait(false);
        using var json = await response.Content.ReadAsJsonAsync(cancellationToken).ConfigureAwait(false);
        var value = json.RootElement.GetSet(entry => entry.GetRecipe(MissingMemberBehavior));
        return (value, new MessageContext(response));
    }
}
