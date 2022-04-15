using System.Net.Http;
using GW2SDK.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Mounts.Http;

[PublicAPI]
public sealed class MountsRequest
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "/v2/mounts/types")
    {
        AcceptEncoding = "gzip"
    };

    public MountsRequest(Language? language)
    {
        Language = language;
    }

    public Language? Language { get; }

    public static implicit operator HttpRequestMessage(MountsRequest r)
    {
        QueryBuilder search = new();
        search.Add("ids", "all");
        var request = Template with
        {
            AcceptLanguage = r.Language?.Alpha2Code,
            Arguments = search
        };
        return request.Compile();
    }
}
