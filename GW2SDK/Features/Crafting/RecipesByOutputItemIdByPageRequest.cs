using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GuildWars2.Http;
using GuildWars2.Json;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GuildWars2.Crafting;

[PublicAPI]
public sealed class RecipesByOutputItemIdByPageRequest : IHttpRequest<IReplicaPage<Recipe>>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "v2/recipes/search")
    {
        AcceptEncoding = "gzip"
    };

    public RecipesByOutputItemIdByPageRequest(int outputItemId, int pageIndex)
    {
        OutputItemId = outputItemId;
        PageIndex = pageIndex;
    }

    public int OutputItemId { get; }

    public int PageIndex { get; }

    public int? PageSize { get; init; }

    public MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<IReplicaPage<Recipe>> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        QueryBuilder search = new()
        {
            { "output", OutputItemId },
            { "page", PageIndex }
        };
        if (PageSize.HasValue)
        {
            search.Add("page_size", PageSize.Value);
        }

        search.Add("v", SchemaVersion.Recommended);
        using var response = await httpClient.SendAsync(
                Template with { Arguments = search },
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);

        using var json = await response.Content.ReadAsJsonAsync(cancellationToken)
            .ConfigureAwait(false);

        var value = json.RootElement.GetSet(entry => RecipeJson.GetRecipe(entry, MissingMemberBehavior));
        return new ReplicaPage<Recipe>(
            response.Headers.Date.GetValueOrDefault(),
            value,
            response.Headers.GetPageContext(),
            response.Content.Headers.Expires,
            response.Content.Headers.LastModified
        );
    }
}
