using System.Collections.Generic;
using System.Net.Http;
using GW2SDK.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.ItemStats.Http
{
    [PublicAPI]
    public sealed class ItemStatsByIdsRequest
    {
        private static readonly HttpRequestMessageTemplate Template = new(Get, "/v2/itemstats")
        {
            AcceptEncoding = "gzip"
        };

        public ItemStatsByIdsRequest(IReadOnlyCollection<int> itemStatIds, Language? language)
        {
            Check.Collection(itemStatIds, nameof(itemStatIds));
            ItemStatIds = itemStatIds;
            Language = language;
        }

        public IReadOnlyCollection<int> ItemStatIds { get; }

        public Language? Language { get; }

        public static implicit operator HttpRequestMessage(ItemStatsByIdsRequest r)
        {
            var search = new QueryBuilder();
            search.Add("ids", r.ItemStatIds);
            var request = Template with
            {
                AcceptLanguage = r.Language?.Alpha2Code,
                Arguments = search
            };
            return request.Compile();
        }
    }
}
