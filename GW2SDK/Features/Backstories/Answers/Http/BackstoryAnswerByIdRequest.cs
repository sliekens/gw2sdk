using System;
using System.Net.Http;
using GW2SDK.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Backstories.Answers.Http
{
    [PublicAPI]
    public sealed class BackstoryAnswerByIdRequest
    {
        public BackstoryAnswerByIdRequest(string answerId, Language? language)
        {
            AnswerId = answerId;
            Language = language;
        }

        public string AnswerId { get; }

        public Language? Language { get; }

        public static implicit operator HttpRequestMessage(BackstoryAnswerByIdRequest r)
        {
            var search = new QueryBuilder();
            search.Add("id", r.AnswerId);
            if (r.Language is not null) search.Add("lang", r.Language.Alpha2Code);
            var location = new Uri($"/v2/backstory/answers?{search}", UriKind.Relative);
            return new HttpRequestMessage(Get, location);
        }
    }
}
