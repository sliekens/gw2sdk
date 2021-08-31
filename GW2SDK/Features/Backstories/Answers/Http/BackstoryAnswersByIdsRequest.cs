using System;
using System.Collections.Generic;
using System.Net.Http;
using GW2SDK.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Backstories.Answers.Http
{
    [PublicAPI]
    public sealed class BackstoryAnswersByIdsRequest
    {
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
            var search = new QueryBuilder();
            search.Add("ids", r.AnswerIds);
            if (r.Language is not null) search.Add("lang", r.Language.Alpha2Code);
            var location = new Uri($"/v2/backstory/answers?{search}", UriKind.Relative);
            return new HttpRequestMessage(Get, location);
        }
    }
}
