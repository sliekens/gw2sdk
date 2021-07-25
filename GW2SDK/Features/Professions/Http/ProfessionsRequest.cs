using System;
using System.Net.Http;
using GW2SDK.Http;
using JetBrains.Annotations;

namespace GW2SDK.Professions.Http
{
    [PublicAPI]
    public sealed class ProfessionsRequest
    {
        public ProfessionsRequest(Language? language)
        {
            Language = language;
        }

        public Language? Language { get; }

        public static implicit operator HttpRequestMessage(ProfessionsRequest r)
        {
            var search = new QueryBuilder();
            search.Add("ids", "all");
            if (r.Language is not null) search.Add("lang", r.Language.Alpha2Code);
            var location = new Uri($"/v2/professions?{search}", UriKind.Relative);
            return new HttpRequestMessage(HttpMethod.Get, location);
        }
    }
}
