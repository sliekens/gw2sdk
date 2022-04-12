using System.Collections.Generic;
using System.Net.Http;

using GW2SDK.Http;

using JetBrains.Annotations;

using static System.Net.Http.HttpMethod;

namespace GW2SDK.Stories.Http;

[PublicAPI]
public sealed class BackstoryQuestionsByIdsRequest
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "/v2/backstory/questions")
    {
        AcceptEncoding = "gzip"
    };

    public BackstoryQuestionsByIdsRequest(IReadOnlyCollection<int> questionIds, Language? language)
    {
        Check.Collection(questionIds, nameof(questionIds));
        QuestionIds = questionIds;
        Language = language;
    }

    public IReadOnlyCollection<int> QuestionIds { get; }

    public Language? Language { get; }

    public static implicit operator HttpRequestMessage(BackstoryQuestionsByIdsRequest r)
    {
        QueryBuilder search = new();
        search.Add("ids", r.QuestionIds);
        var request = Template with
        {
            AcceptLanguage = r.Language?.Alpha2Code,
            Arguments = search
        };
        return request.Compile();
    }
}