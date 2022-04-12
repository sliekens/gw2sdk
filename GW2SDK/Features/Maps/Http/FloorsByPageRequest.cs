using System.Globalization;
using System.Net.Http;

using GW2SDK.Http;

using JetBrains.Annotations;

using static System.Net.Http.HttpMethod;

namespace GW2SDK.Maps.Http;

[PublicAPI]
public sealed class FloorsByPageRequest
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "/v2/continents/:id/floors")
    {
        AcceptEncoding = "gzip"
    };

    public FloorsByPageRequest(
        int continentId,
        int pageIndex,
        int? pageSize,
        Language? language
    )
    {
        ContinentId = continentId;
        PageIndex = pageIndex;
        PageSize = pageSize;
        Language = language;
    }

    public int ContinentId { get; }

    public int PageIndex { get; }

    public int? PageSize { get; }

    public Language? Language { get; }

    public static implicit operator HttpRequestMessage(FloorsByPageRequest r)
    {
        QueryBuilder search = new();
        search.Add("page", r.PageIndex);
        if (r.PageSize.HasValue)
        {
            search.Add("page_size", r.PageSize.Value);
        }

        var request = Template with
        {
            Path = Template.Path.Replace(":id", r.ContinentId.ToString(CultureInfo.InvariantCulture)),
            AcceptLanguage = r.Language?.Alpha2Code,
            Arguments = search
        };
        return request.Compile();
    }
}