using System;
using System.Collections.Generic;
using System.Net.Http;
using GW2SDK.Annotations;
using GW2SDK.Http;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Skins.Http
{
    [PublicAPI]
    public sealed class SkinsByIdsRequest
    {
        public SkinsByIdsRequest(IReadOnlyCollection<int> skinIds)
        {
            if (skinIds is null)
            {
                throw new ArgumentNullException(nameof(skinIds));
            }

            if (skinIds.Count == 0)
            {
                throw new ArgumentException("Skin IDs cannot be an empty collection.", nameof(skinIds));
            }

            SkinIds = skinIds;
        }

        public IReadOnlyCollection<int> SkinIds { get; }

        public static implicit operator HttpRequestMessage(SkinsByIdsRequest r)
        {
            var search = new QueryBuilder();
            search.Add("ids", r.SkinIds);
            var location = new Uri($"/v2/skins?{search}", UriKind.Relative);
            return new HttpRequestMessage(Get, location);
        }
    }
}
