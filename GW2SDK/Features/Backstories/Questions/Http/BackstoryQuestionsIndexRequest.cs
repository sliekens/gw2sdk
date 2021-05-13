using System;
using System.Net.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Backstories.Questions.Http
{
    [PublicAPI]
    public sealed class BackstoryQuestionsIndexRequest
    {
        public static implicit operator HttpRequestMessage(BackstoryQuestionsIndexRequest _)
        {
            var location = new Uri("/v2/backstory/questions", UriKind.Relative);
            return new HttpRequestMessage(Get, location);
        }
    }
}
