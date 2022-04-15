using System.Net.Http;
using GW2SDK.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Stories.Http;

[PublicAPI]
public sealed class BackstoryAnswerByIdRequest
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "/v2/backstory/answers")
    {
        AcceptEncoding = "gzip"
    };

    public BackstoryAnswerByIdRequest(string answerId, Language? language)
    {
        AnswerId = answerId;
        Language = language;
    }

    public string AnswerId { get; }

    public Language? Language { get; }

    public static implicit operator HttpRequestMessage(BackstoryAnswerByIdRequest r)
    {
        QueryBuilder search = new();
        search.Add("id", r.AnswerId);
        var request = Template with
        {
            AcceptLanguage = r.Language?.Alpha2Code,
            Arguments = search
        };
        return request.Compile();
    }
}
