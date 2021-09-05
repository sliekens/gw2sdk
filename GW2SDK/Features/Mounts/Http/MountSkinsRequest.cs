using System.Net.Http;
using GW2SDK.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Mounts.Http
{
    [PublicAPI]
    public sealed class MountSkinsRequest
    {
        private static readonly HttpRequestMessageTemplate Template = new(Get, "/v2/mounts/skins")
        {
            AcceptEncoding = "gzip"
        };

        public MountSkinsRequest(Language? language)
        {
            Language = language;
        }

        public Language? Language { get; }

        public static implicit operator HttpRequestMessage(MountSkinsRequest r)
        {
            var search = new QueryBuilder();
            search.Add("ids", "all");
            var request = Template with
            {
                AcceptLanguage = r.Language?.Alpha2Code,
                Arguments = search
            };
            return request.Compile();
        }
    }
}
