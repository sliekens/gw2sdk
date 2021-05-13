using System;
using System.Net.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Backstories.Answers.Http
{
    [PublicAPI]
    public sealed class BackstoryAnswersIndexRequest
    {
        public static implicit operator HttpRequestMessage(BackstoryAnswersIndexRequest _)
        {
            var location = new Uri("/v2/backstory/answers", UriKind.Relative);
            return new HttpRequestMessage(Get, location);
        }
    }
}
