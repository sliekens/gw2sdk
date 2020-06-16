using System;
using System.Net.Http;
using GW2SDK.Impl;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Backstories.Answers.Impl
{
    public sealed class BackstoryAnswerByIdRequest
    {
        public BackstoryAnswerByIdRequest(string answerId)
        {
            AnswerId = answerId;
        }

        public string AnswerId { get; }

        public static implicit operator HttpRequestMessage(BackstoryAnswerByIdRequest r)
        {
            var search = new QueryBuilder();
            search.Add("id", r.AnswerId);
            var location = new Uri($"/v2/backstory/answers?{search}", UriKind.Relative);
            return new HttpRequestMessage(Get, location);
        }
    }
}
