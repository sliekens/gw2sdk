using System;
using System.Collections.Generic;
using System.Net.Http;
using GW2SDK.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Masteries.Http
{
    [PublicAPI]
    public sealed class MasteriesByIdsRequest
    {
        public MasteriesByIdsRequest(IReadOnlyCollection<int> masteryIds, Language? language)
        {
            Check.Collection(masteryIds, nameof(masteryIds));
            MasteryIds = masteryIds;
            Language = language;
        }

        public IReadOnlyCollection<int> MasteryIds { get; }

        public Language? Language { get; }

        public static implicit operator HttpRequestMessage(MasteriesByIdsRequest r)
        {
            var search = new QueryBuilder();
            search.Add("ids", r.MasteryIds);
            if (r.Language is not null) search.Add("lang", r.Language.Alpha2Code);
            var location = new Uri($"/v2/masteries?{search}", UriKind.Relative);
            return new HttpRequestMessage(Get, location);
        }
    }
}
