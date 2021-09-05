using System.Net.Http;
using GW2SDK.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Backstories.Answers.Http
{
    [PublicAPI]
    public sealed class BackstoryAnswersIndexRequest
    {
        private static readonly HttpRequestMessageTemplate Template = new(Get, "/v2/backstory/answers")
        {
            AcceptEncoding = "gzip"
        };

        public static implicit operator HttpRequestMessage(BackstoryAnswersIndexRequest _) => Template.Compile();
    }
}
