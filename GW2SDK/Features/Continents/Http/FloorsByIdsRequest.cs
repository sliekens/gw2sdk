using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using GW2SDK.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Continents.Http
{
    [PublicAPI]
    public sealed class FloorsByIdsRequest
    {
        private static readonly HttpRequestMessageTemplate Template = new(Get, "/v2/continents/:id/floors")
        {
            AcceptEncoding = "gzip"
        };

        public FloorsByIdsRequest(
            int continentId,
            IReadOnlyCollection<int> floorIds,
            Language? language
        )
        {
            Check.Collection(floorIds, nameof(floorIds));
            ContinentId = continentId;
            FloorIds = floorIds;
            Language = language;
        }

        public int ContinentId { get; }

        public IReadOnlyCollection<int> FloorIds { get; }

        public Language? Language { get; }

        public static implicit operator HttpRequestMessage(FloorsByIdsRequest r)
        {
            var search = new QueryBuilder();
            search.Add("ids", r.FloorIds);
            var request = Template with
            {
                Path = Template.Path.Replace(":id", r.ContinentId.ToString(CultureInfo.InvariantCulture)),
                AcceptLanguage = r.Language?.Alpha2Code,
                Arguments = search
            };
            return request.Compile();
        }
    }
}
