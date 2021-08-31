using System;
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
            if (r.Language is not null) search.Add("lang", r.Language.Alpha2Code);
            var location = new Uri($"/v2/skins?{search}", UriKind.Relative);
            return new HttpRequestMessage(Get, location);
        }
    }
}
