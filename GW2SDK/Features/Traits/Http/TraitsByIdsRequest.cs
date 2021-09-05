using System.Collections.Generic;
using System.Net.Http;
using GW2SDK.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Traits.Http
{
    [PublicAPI]
    public sealed class TraitsByIdsRequest
    {
        private static readonly HttpRequestMessageTemplate Template = new(Get, "/v2/traits")
        {
            AcceptEncoding = "gzip"
        };

        public TraitsByIdsRequest(IReadOnlyCollection<int> traitIds, Language? language)
        {
            Check.Collection(traitIds, nameof(traitIds));
            TraitIds = traitIds;
            Language = language;
        }

        public IReadOnlyCollection<int> TraitIds { get; }

        public Language? Language { get; }

        public static implicit operator HttpRequestMessage(TraitsByIdsRequest r)
        {
            var search = new QueryBuilder();
            search.Add("ids", r.TraitIds);
            var request = Template with
            {
                AcceptLanguage = r.Language?.Alpha2Code,
                Arguments = search
            };
            return request.Compile();
        }
    }
}
