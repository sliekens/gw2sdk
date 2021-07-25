using System;
using System.Net.Http;
using GW2SDK.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Specializations.Http
{
    [PublicAPI]
    public sealed class SpecializationByIdRequest
    {
        public SpecializationByIdRequest(int specializationId, Language? language)
        {
            SpecializationId = specializationId;
            Language = language;
        }

        public int SpecializationId { get; }

        public Language? Language { get; }

        public static implicit operator HttpRequestMessage(SpecializationByIdRequest r)
        {
            var search = new QueryBuilder();
            search.Add("id", r.SpecializationId);
            if (r.Language is not null) search.Add("lang", r.Language.Alpha2Code);
            var location = new Uri($"/v2/specializations?{search}", UriKind.Relative);
            return new HttpRequestMessage(Get, location);
        }
    }
}
