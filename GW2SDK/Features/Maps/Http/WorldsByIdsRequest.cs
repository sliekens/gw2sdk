using System.Collections.Generic;
using System.Net.Http;
using GW2SDK.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Maps.Http;

[PublicAPI]
public sealed class WorldsByIdsRequest
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "/v2/worlds")
    {
        AcceptEncoding = "gzip"
    };

    public WorldsByIdsRequest(IReadOnlyCollection<int> worldIds, Language? language)
    {
        Check.Collection(worldIds, nameof(worldIds));
        WorldIds = worldIds;
        Language = language;
    }

    public IReadOnlyCollection<int> WorldIds { get; }

    public Language? Language { get; }

    public static implicit operator HttpRequestMessage(WorldsByIdsRequest r)
    {
        QueryBuilder search = new();
        search.Add("ids", r.WorldIds);
        var request = Template with
        {
            AcceptLanguage = r.Language?.Alpha2Code,
            Arguments = search
        };
        return request.Compile();
    }
}
