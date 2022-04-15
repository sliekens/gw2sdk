using System.Collections.Generic;
using System.Net.Http;
using GW2SDK.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Maps.Http;

[PublicAPI]
public sealed class ContinentsByIdsRequest
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "/v2/continents")
    {
        AcceptEncoding = "gzip"
    };

    public ContinentsByIdsRequest(IReadOnlyCollection<int> continentIds, Language? language)
    {
        Check.Collection(continentIds, nameof(continentIds));
        ContinentIds = continentIds;
        Language = language;
    }

    public IReadOnlyCollection<int> ContinentIds { get; }

    public Language? Language { get; }

    public static implicit operator HttpRequestMessage(ContinentsByIdsRequest r)
    {
        QueryBuilder search = new();
        search.Add("ids", r.ContinentIds);
        var request = Template with
        {
            AcceptLanguage = r.Language?.Alpha2Code,
            Arguments = search
        };
        return request.Compile();
    }
}
