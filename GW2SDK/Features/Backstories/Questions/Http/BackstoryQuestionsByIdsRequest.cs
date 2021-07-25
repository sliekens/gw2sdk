using System;
using System.Collections.Generic;
using System.Net.Http;
using GW2SDK.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Backstories.Questions.Http
{
    [PublicAPI]
    public sealed class BackstoryQuestionsByIdsRequest
    {
        public BackstoryQuestionsByIdsRequest(IReadOnlyCollection<int> questionIds, Language? language)
        {
            if (questionIds is null)
            {
                throw new ArgumentNullException(nameof(questionIds));
            }

            if (questionIds.Count == 0)
            {
                throw new ArgumentException("Backstory question IDs cannot be an empty collection.",
                    nameof(questionIds));
            }

            QuestionIds = questionIds;
            Language = language;
        }

        public IReadOnlyCollection<int> QuestionIds { get; }

        public Language? Language { get; }

        public static implicit operator HttpRequestMessage(BackstoryQuestionsByIdsRequest r)
        {
            var search = new QueryBuilder();
            search.Add("ids", r.QuestionIds);
            if (r.Language is not null) search.Add("lang", r.Language.Alpha2Code);
            var location = new Uri($"/v2/backstory/questions?{search}", UriKind.Relative);
            return new HttpRequestMessage(Get, location);
        }
    }
}
