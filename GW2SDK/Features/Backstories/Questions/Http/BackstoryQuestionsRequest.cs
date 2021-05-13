using System;
using System.Net.Http;
using GW2SDK.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Backstories.Questions.Http
{
    [PublicAPI]
    public sealed class BackstoryQuestionsRequest
    {
        public static implicit operator HttpRequestMessage(BackstoryQuestionsRequest _)
        {
            var location = new Uri("/v2/backstory/questions?ids=all", UriKind.Relative);
            return new HttpRequestMessage(Get, location);
        }
    }
}
