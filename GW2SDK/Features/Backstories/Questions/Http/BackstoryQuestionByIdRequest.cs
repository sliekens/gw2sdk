using System;
using System.Net.Http;
using JetBrains.Annotations;
using GW2SDK.Http;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Backstories.Questions.Http
{
    [PublicAPI]
    public sealed class BackstoryQuestionByIdRequest
    {
        public BackstoryQuestionByIdRequest(int questionId)
        {
            QuestionId = questionId;
        }

        public int QuestionId { get; }

        public static implicit operator HttpRequestMessage(BackstoryQuestionByIdRequest r)
        {
            var search = new QueryBuilder();
            search.Add("id", r.QuestionId);
            var location = new Uri($"/v2/backstory/questions?{search}", UriKind.Relative);
            return new HttpRequestMessage(Get, location);
        }
    }
}
