using System;
using System.Collections.Generic;
using System.Linq;
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
            if (answerIds is null)
            {
                throw new ArgumentNullException(nameof(answerIds));
            }

            if (answerIds.Count == 0)
            {
                throw new ArgumentException("Backstory answer IDs cannot be an empty collection.", nameof(answerIds));
            }

            if (answerIds.Any(string.IsNullOrEmpty))
            {
                throw new ArgumentException("Backstory answer IDs collection cannot contain empty values.",
                    nameof(answerIds));
            }

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
