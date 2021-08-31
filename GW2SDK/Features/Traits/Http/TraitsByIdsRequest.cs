using System;
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
            if (r.Language is not null) search.Add("lang", r.Language.Alpha2Code);
            var location = new Uri($"/v2/traits?{search}", UriKind.Relative);
            return new HttpRequestMessage(Get, location);
        }
    }
}
