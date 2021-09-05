using System.Net.Http;
using GW2SDK.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Professions.Http
{
    [PublicAPI]
    public sealed class ProfessionByNameRequest
    {
        private static readonly HttpRequestMessageTemplate Template = new(Get, "/v2/professions")
        {
            AcceptEncoding = "gzip"
        };

        public ProfessionByNameRequest(ProfessionName professionName, Language? language)
        {
            Check.Constant(professionName, nameof(professionName));
            ProfessionName = professionName;
            Language = language;
        }

        public ProfessionName ProfessionName { get; }

        public Language? Language { get; }

        public static implicit operator HttpRequestMessage(ProfessionByNameRequest r)
        {
            var search = new QueryBuilder();
            search.Add("id", r.ProfessionName.ToString());
            var request = Template with
            {
                AcceptLanguage = r.Language?.Alpha2Code,
                Arguments = search
            };
            return request.Compile();
        }
    }
}
