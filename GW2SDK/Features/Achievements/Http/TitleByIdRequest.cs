using System.Net.Http;

using GW2SDK.Http;

using JetBrains.Annotations;

using static System.Net.Http.HttpMethod;

namespace GW2SDK.Achievements.Http;

[PublicAPI]
public sealed class TitleByIdRequest
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "/v2/titles")
    {
        AcceptEncoding = "gzip"
    };

    public TitleByIdRequest(int titleId, Language? language)
    {
        TitleId = titleId;
        Language = language;
    }

    public int TitleId { get; }

    public Language? Language { get; }

    public static implicit operator HttpRequestMessage(TitleByIdRequest r)
    {
        QueryBuilder search = new();
        search.Add("id", r.TitleId);
        var request = Template with
        {
            AcceptLanguage = r.Language?.Alpha2Code,
            Arguments = search
        };
        return request.Compile();
    }
}