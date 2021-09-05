using System.Net.Http;
using GW2SDK.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Titles.Http
{
    [PublicAPI]
    public sealed class TitlesIndexRequest
    {
        private static readonly HttpRequestMessageTemplate Template = new(Get, "/v2/titles")
        {
            AcceptEncoding = "gzip"
        };

        public static implicit operator HttpRequestMessage(TitlesIndexRequest _) => Template.Compile();
    }
}
