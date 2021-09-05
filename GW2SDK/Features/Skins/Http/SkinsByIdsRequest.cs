using System.Collections.Generic;
using System.Net.Http;
using GW2SDK.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Skins.Http
{
    [PublicAPI]
    public sealed class SkinsByIdsRequest
    {
        private static readonly HttpRequestMessageTemplate Template = new(Get, "/v2/skins")
        {
            AcceptEncoding = "gzip"
        };

        public SkinsByIdsRequest(IReadOnlyCollection<int> skinIds, Language? language)
        {
            Check.Collection(skinIds, nameof(skinIds));
            SkinIds = skinIds;
            Language = language;
        }

        public IReadOnlyCollection<int> SkinIds { get; }

        public Language? Language { get; }

        public static implicit operator HttpRequestMessage(SkinsByIdsRequest r)
        {
            var search = new QueryBuilder();
            search.Add("ids", r.SkinIds);
            var request = Template with
            {
                AcceptLanguage = r.Language?.Alpha2Code,
                Arguments = search
            };
            return request.Compile();
        }
    }
}
