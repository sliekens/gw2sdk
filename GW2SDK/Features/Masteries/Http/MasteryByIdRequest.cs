using System.Net.Http;
using GW2SDK.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Masteries.Http
{
    [PublicAPI]
    public sealed class MasteryByIdRequest
    {
        private static readonly HttpRequestMessageTemplate Template = new(Get, "/v2/masteries")
        {
            AcceptEncoding = "gzip"
        };

        public MasteryByIdRequest(int masteryId, Language? language)
        {
            MasteryId = masteryId;
            Language = language;
        }

        public int MasteryId { get; }

        public Language? Language { get; }

        public static implicit operator HttpRequestMessage(MasteryByIdRequest r)
        {
            var search = new QueryBuilder();
            search.Add("id", r.MasteryId);
            var request = Template with
            {
                AcceptLanguage = r.Language?.Alpha2Code,
                Arguments = search
            };
            return request.Compile();
        }
    }
}
