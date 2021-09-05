using System;
using System.Net.Http;
using GW2SDK.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Items.Http
{
    [PublicAPI]
    public sealed class ItemByIdRequest
    {
        private static readonly HttpRequestMessageTemplate Template = new(Get, "/v2/items")
        {
            AcceptEncoding = "gzip"
        };
        public ItemByIdRequest(int itemId, Language? language)
        {
            ItemId = itemId;
            Language = language;
        }

        public int ItemId { get; }

        public Language? Language { get; }

        public static implicit operator HttpRequestMessage(ItemByIdRequest r)
        {
            var search = new QueryBuilder();
            search.Add("id", r.ItemId);
            var request = Template with
            {
                AcceptLanguage = r.Language?.Alpha2Code,
                Arguments = search
            };
            return request.Compile();
        }
    }
}
