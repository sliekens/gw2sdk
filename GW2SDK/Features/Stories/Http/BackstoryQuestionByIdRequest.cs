using System.Net.Http;

using GW2SDK.Http;

using JetBrains.Annotations;

using static System.Net.Http.HttpMethod;

namespace GW2SDK.Stories.Http;

[PublicAPI]
public sealed class BackstoryQuestionByIdRequest
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "/v2/backstory/questions")
    {
        AcceptEncoding = "gzip"
    };

    public BackstoryQuestionByIdRequest(int questionId, Language? language)
    {
        QuestionId = questionId;
        Language = language;
    }

    public int QuestionId { get; }

    public Language? Language { get; }

    public static implicit operator HttpRequestMessage(BackstoryQuestionByIdRequest r)
    {
        QueryBuilder search = new();
        search.Add("id", r.QuestionId);
        var request = Template with
        {
            AcceptLanguage = r.Language?.Alpha2Code,
            Arguments = search
        };
        return request.Compile();
    }
}