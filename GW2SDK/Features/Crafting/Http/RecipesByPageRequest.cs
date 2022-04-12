using System.Net.Http;

using GW2SDK.Http;

using JetBrains.Annotations;

using static System.Net.Http.HttpMethod;

namespace GW2SDK.Crafting.Http;

[PublicAPI]
public sealed class RecipesByPageRequest
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "/v2/recipes")
    {
        AcceptEncoding = "gzip"
    };

    public RecipesByPageRequest(int pageIndex, int? pageSize)
    {
        PageIndex = pageIndex;
        PageSize = pageSize;
    }

    public int PageIndex { get; }

    public int? PageSize { get; }

    public static implicit operator HttpRequestMessage(RecipesByPageRequest r)
    {
        QueryBuilder search = new();
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