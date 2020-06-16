using System;
using System.Collections.Generic;
using System.Net.Http;
using GW2SDK.Impl;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Backstories.Answers.Impl
{
    public sealed class BackstoryAnswersByIdsRequest
    {
        public BackstoryAnswersByIdsRequest(IReadOnlyCollection<string> answerIds)
        {
            if (answerIds == null)
            {
                throw new ArgumentNullException(nameof(answerIds));
            }

            if (answerIds.Count == 0)
            {
                throw new ArgumentException("Backstory answer IDs cannot be an empty collection.", nameof(answerIds));
            }

            AnswerIds = answerIds;
        }

        public IReadOnlyCollection<string> AnswerIds { get; }

        public static implicit operator HttpRequestMessage(BackstoryAnswersByIdsRequest r)
        {
            var search = new QueryBuilder();
            search.Add("ids", r.AnswerIds);
            var location = new Uri($"/v2/backstory/answers?{search}", UriKind.Relative);
            return new HttpRequestMessage(Get, location);
        }
    }
}
