using System;
using System.Net.Http;
using GW2SDK.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Backstories.Questions.Http
{
    [PublicAPI]
    public sealed class BackstoryQuestionByIdRequest
    {
        public BackstoryQuestionByIdRequest(int questionId, Language? language)
        {
            QuestionId = questionId;
            Language = language;
        }

        public int QuestionId { get; }

        public Language? Language { get; }

        public static implicit operator HttpRequestMessage(BackstoryQuestionByIdRequest r)
        {
            var search = new QueryBuilder();
            search.Add("id", r.QuestionId);
            if (r.Language is not null) search.Add("lang", r.Language.Alpha2Code);
            var location = new Uri($"/v2/backstory/questions?{search}", UriKind.Relative);
            return new HttpRequestMessage(Get, location);
        }
    }
}
