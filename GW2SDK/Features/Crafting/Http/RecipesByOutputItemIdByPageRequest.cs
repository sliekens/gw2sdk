using System.Net.Http;

using GW2SDK.Http;

using JetBrains.Annotations;

using static System.Net.Http.HttpMethod;

namespace GW2SDK.Crafting.Http;

[PublicAPI]
public sealed class RecipesByOutputItemIdByPageRequest
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "/v2/recipes/search")
    {
        AcceptEncoding = "gzip"
    };

    public RecipesByOutputItemIdByPageRequest(
        int outputItemId,
        int pageIndex,
        int? pageSize
    )
    {
        OutputItemId = outputItemId;
        PageIndex = pageIndex;
        PageSize = pageSize;
    }

    public int OutputItemId { get; }

    public int PageIndex { get; }

    public int? PageSize { get; }

    public static implicit operator HttpRequestMessage(RecipesByOutputItemIdByPageRequest r)
    {
        QueryBuilder search = new();
        search.Add("output", r.OutputItemId);
        search.Add("page", r.PageIndex);
        if (r.PageSize.HasValue)
        {
            search.Add("page_size", r.PageSize.Value);
        }

        var request = Template with
        {
            Arguments = search
        };
        return request.Compile();
    }
}