using System;
using System.Net.Http;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Backstories.Questions.Impl
{
    public sealed class BackstoryQuestionsRequest
    {
        public static implicit operator HttpRequestMessage(BackstoryQuestionsRequest _)
        {
            var location = new Uri("/v2/backstory/questions?ids=all", UriKind.Relative);
            return new HttpRequestMessage(Get, location);
        }
    }
}
