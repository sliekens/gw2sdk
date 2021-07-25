using System;
using System.Net.Http;
using GW2SDK.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.ItemStats.Http
{
    [PublicAPI]
    public sealed class ItemStatByIdRequest
    {
        public ItemStatByIdRequest(int itemStatId, Language? language)
        {
            ItemStatId = itemStatId;
            Language = language;
        }

        public int ItemStatId { get; }

        public Language? Language { get; }

        public static implicit operator HttpRequestMessage(ItemStatByIdRequest r)
        {
            var search = new QueryBuilder();
            search.Add("id", r.ItemStatId);
            if (r.Language is not null) search.Add("lang", r.Language.Alpha2Code);
            var location = new Uri($"/v2/itemstats?{search}", UriKind.Relative);
            return new HttpRequestMessage(Get, location);
        }
    }
}
