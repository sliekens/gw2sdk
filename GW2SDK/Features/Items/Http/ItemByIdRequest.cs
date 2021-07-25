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
            if (r.Language is not null) search.Add("lang", r.Language.Alpha2Code);
            var location = new Uri($"/v2/items?{search}", UriKind.Relative);
            return new HttpRequestMessage(Get, location);
        }
    }
}
