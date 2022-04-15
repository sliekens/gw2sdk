using System.Net.Http;
using GW2SDK.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Stories.Http;

[PublicAPI]
public sealed class BackstoryAnswersRequest
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "/v2/backstory/answers")
    {
        AcceptEncoding = "gzip"
    };

    public BackstoryAnswersRequest(Language? language)
    {
        Language = language;
    }

    public Language? Language { get; }

    public static implicit operator HttpRequestMessage(BackstoryAnswersRequest r)
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
