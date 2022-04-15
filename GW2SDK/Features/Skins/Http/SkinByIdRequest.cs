using System.Net.Http;
using GW2SDK.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Skins.Http;

[PublicAPI]
public sealed class SkinByIdRequest
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "/v2/skins")
    {
        AcceptEncoding = "gzip"
    };

    public SkinByIdRequest(int skinId, Language? language)
    {
        SkinId = skinId;
        Language = language;
    }

    public int SkinId { get; }

    public Language? Language { get; }

    public static implicit operator HttpRequestMessage(SkinByIdRequest r)
    {
        QueryBuilder search = new();
        search.Add("id", r.SkinId);
        var request = Template with
        {
            AcceptLanguage = r.Language?.Alpha2Code,
            Arguments = search
        };
        return request.Compile();
    }
}
