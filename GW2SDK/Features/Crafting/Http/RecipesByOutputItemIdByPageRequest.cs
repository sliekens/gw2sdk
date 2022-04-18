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
public sealed class RecipesByOutputItemIdByPageRequest : IHttpRequest<IReplicaPage<Recipe>>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "/v2/recipes/search")
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
        QueryBuilder search = new();
        search.Add("output", OutputItemId);
        search.Add("page", PageIndex);
        if (PageSize.HasValue)
        {
            search.Add("page_size", PageSize.Value);
        }

        var request = Template with { Arguments = search };

        using var response = await httpClient.SendAsync(
                request.Compile(),
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken
                )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);

        using var json = await response.Content.ReadAsJsonAsync(cancellationToken)
            .ConfigureAwait(false);

        var value =
            json.RootElement.GetSet(entry => RecipeReader.Read(entry, MissingMemberBehavior));
        return new ReplicaPage<Recipe>(
            response.Headers.Date.GetValueOrDefault(),
            value,
            response.Headers.GetPageContext(),
            response.Content.Headers.Expires,
            response.Content.Headers.LastModified
            );
    }
}
