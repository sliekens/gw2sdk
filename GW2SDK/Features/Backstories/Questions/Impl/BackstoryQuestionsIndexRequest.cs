using System;
using System.Net.Http;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Backstories.Questions.Impl
{
    public sealed class BackstoryQuestionsIndexRequest
    {
        public static implicit operator HttpRequestMessage(BackstoryQuestionsIndexRequest _)
        {
            var location = new Uri("/v2/backstory/questions", UriKind.Relative);
            return new HttpRequestMessage(Get, location);
        }
    }
}
