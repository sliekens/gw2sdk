using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.Crafting;

[PublicAPI]
public sealed class RecipesIndexByIngredientItemIdRequest : IHttpRequest<Replica<HashSet<int>>>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "v2/recipes/search")
    {
        AcceptEncoding = "gzip"
    };

    public RecipesIndexByIngredientItemIdRequest(int ingredientItemId)
    {
        IngredientItemId = ingredientItemId;
    }

    public int IngredientItemId { get; }

    public async Task<Replica<HashSet<int>>> SendAsync(
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
        return new Replica<HashSet<int>>
        {
            Value = json.RootElement.GetSet(entry => entry.GetInt32()),
            ResultContext = response.Headers.GetResultContext(),
            PageContext = response.Headers.GetPageContext(),
            Date = response.Headers.Date.GetValueOrDefault(),
            Expires = response.Content.Headers.Expires,
            LastModified = response.Content.Headers.LastModified
        };
    }
}
