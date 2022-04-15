using System.Net.Http;
using GW2SDK.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Colors.Http;

[PublicAPI]
public sealed class ColorsRequest
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "/v2/colors")
    {
        AcceptEncoding = "gzip"
    };

    public ColorsRequest(Language? language)
    {
        Language = language;
    }

    public Language? Language { get; }

    public static implicit operator HttpRequestMessage(ColorsRequest r)
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
