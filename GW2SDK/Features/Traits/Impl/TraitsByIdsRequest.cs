using System;
using System.Collections.Generic;
using System.Net.Http;
using GW2SDK.Impl;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Traits.Impl
{
    public sealed class TraitsByIdsRequest
    {
        public TraitsByIdsRequest(IReadOnlyCollection<int> traitIds)
        {
            if (traitIds is null)
            {
                throw new ArgumentNullException(nameof(traitIds));
            }

            if (traitIds.Count == 0)
            {
                throw new ArgumentException("Trait IDs cannot be an empty collection.", nameof(traitIds));
            }

            TraitIds = traitIds;
        }

        public IReadOnlyCollection<int> TraitIds { get; }

        public static implicit operator HttpRequestMessage(TraitsByIdsRequest r)
        {
            var search = new QueryBuilder();
            search.Add("ids", r.TraitIds);
            var location = new Uri($"/v2/traits?{search}", UriKind.Relative);
            return new HttpRequestMessage(Get, location);
        }
    }
}
