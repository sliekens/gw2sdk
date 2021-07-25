using System;
using System.Collections.Generic;
using System.Net.Http;
using GW2SDK.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Specializations.Http
{
    [PublicAPI]
    public sealed class SpecializationsByIdsRequest
    {
        public SpecializationsByIdsRequest(IReadOnlyCollection<int> specializationIds, Language? language)
        {
            if (specializationIds is null)
            {
                throw new ArgumentNullException(nameof(specializationIds));
            }

            if (specializationIds.Count == 0)
            {
                throw new ArgumentException("Specialization IDs cannot be an empty collection.", nameof(specializationIds));
            }

            SpecializationIds = specializationIds;
            Language = language;
        }

        public IReadOnlyCollection<int> SpecializationIds { get; }

        public Language? Language { get; }

        public static implicit operator HttpRequestMessage(SpecializationsByIdsRequest r)
        {
            var search = new QueryBuilder();
            search.Add("ids", r.SpecializationIds);
            if (r.Language is not null) search.Add("lang", r.Language.Alpha2Code);
            var location = new Uri($"/v2/specializations?{search}", UriKind.Relative);
            return new HttpRequestMessage(Get, location);
        }
    }
}
