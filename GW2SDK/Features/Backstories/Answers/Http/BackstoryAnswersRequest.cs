using System;
using System.Net.Http;
using GW2SDK.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Backstories.Answers.Http
{
    [PublicAPI]
    public sealed class BackstoryAnswersRequest
    {
        public static implicit operator HttpRequestMessage(BackstoryAnswersRequest _)
        {
            var location = new Uri("/v2/backstory/answers?ids=all", UriKind.Relative);
            return new HttpRequestMessage(Get, location);
        }
    }
}
