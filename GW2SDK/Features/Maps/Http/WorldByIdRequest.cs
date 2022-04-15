using System.Net.Http;
using GW2SDK.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Maps.Http;

[PublicAPI]
public sealed class WorldByIdRequest
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "/v2/worlds")
    {
        AcceptEncoding = "gzip"
    };

    public WorldByIdRequest(int worldId, Language? language)
    {
        WorldId = worldId;
        Language = language;
    }

    public int WorldId { get; }

    public Language? Language { get; }

    public static implicit operator HttpRequestMessage(WorldByIdRequest r)
    {
        QueryBuilder search = new();
        search.Add("id", r.WorldId);
        var request = Template with
        {
            AcceptLanguage = r.Language?.Alpha2Code,
            Arguments = search
        };
        return request.Compile();
    }
}
