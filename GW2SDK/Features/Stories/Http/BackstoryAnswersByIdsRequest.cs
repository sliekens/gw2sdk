using System.Collections.Generic;
using System.Net.Http;
using GW2SDK.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Stories.Http;

[PublicAPI]
public sealed class BackstoryAnswersByIdsRequest
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "/v2/backstory/answers")
    {
        AcceptEncoding = "gzip"
    };

    public BackstoryAnswersByIdsRequest(IReadOnlyCollection<string> answerIds, Language? language)
    {
        Check.Collection(answerIds, nameof(answerIds));
        AnswerIds = answerIds;
        Language = language;
    }

    public IReadOnlyCollection<string> AnswerIds { get; }

    public Language? Language { get; }

    public static implicit operator HttpRequestMessage(BackstoryAnswersByIdsRequest r)
    {
        QueryBuilder search = new();
        search.Add("ids", r.AnswerIds);
        var request = Template with
        {
            AcceptLanguage = r.Language?.Alpha2Code,
            Arguments = search
        };
        return request.Compile();
    }
}
